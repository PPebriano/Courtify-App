using CourtifyBE.DTOs;
using CourtifyBE.Exceptions;
using CourtifyBE.Models;
using CourtifyBE.Repositories;

namespace CourtifyBE.Services
{
    public class BookingService : IBookingService
    {
        private readonly IRepository<Bookings> _bookingRepository;
        private readonly IRepository<Courts> _courtRepository;
        private readonly IRepository<EquipmentAddOns> _equipmentRepository;
        private readonly IRepository<BookingAddOns> _bookingAddOnsRepository;
        private readonly ILogger<BookingService> _logger;

        public BookingService(
            IRepository<Bookings> bookingRepository,
            IRepository<Courts> courtRepository,
            IRepository<EquipmentAddOns> equipmentRepository,
            IRepository<BookingAddOns> bookingAddOnsRepository,
            ILogger<BookingService> logger)
        {
            _bookingRepository = bookingRepository;
            _courtRepository = courtRepository;
            _equipmentRepository = equipmentRepository;
            _bookingAddOnsRepository = bookingAddOnsRepository;
            _logger = logger;
        }

        public async Task<Bookings?> GetByIdAsync(long id)
        {
            return await _bookingRepository.GetByIdAsync(id, "Courts", "BookingAddOns.Equipment", "PaymentReceipts");
        }

        public async Task<List<Bookings>> GetAllAsync(string? status, DateTime? createdAt)
        {
            var query = await _bookingRepository.GetAllAsync();
            var bookings = query.AsQueryable();

            if (!string.IsNullOrEmpty(status))
                bookings = bookings.Where(b => b.Status.ToString().Equals(status, StringComparison.OrdinalIgnoreCase));

            if (createdAt.HasValue)
                bookings = bookings.Where(b => b.CreatedAt.Date == createdAt.Value.Date);

            return bookings.ToList();
        }

        public async Task<BookingListResponse> CreateFullTransactionAsync(CreateBookingRequest request, long currentAdminId)
        {
            _logger.LogInformation("Memulai pembuatan transaksi booking untuk Lapangan ID: {CourtId} oleh Admin ID: {AdminId}", request.CourtId, currentAdminId);
            
            var court = await _courtRepository.GetByIdAsync(request.CourtId);
            if (court == null)
            {
                _logger.LogWarning("Pembuatan booking gagal: Lapangan dengan ID {CourtId} tidak ditemukan", request.CourtId);
                throw new Exception("Lapangan tidak ditemukan");
            }
            int total_hours = (int)(request.EndTime - request.StartTime).TotalHours;
            if (total_hours <= 0)
            {
                _logger.LogWarning("Pembuatan booking gagal: Waktu sewa tidak valid (Mulai: {StartTime}, Selesai: {EndTime})", request.StartTime, request.EndTime);
                throw new Exception("Waktu sewa tidak valid");
            }
            var allBookings = await _bookingRepository.GetAllAsync();
            var isCourtOccupied = allBookings.Any(b =>
                b.CourtId == request.CourtId &&
                b.BookingDate.Date == request.BookingDate.Date &&
                b.Status != BookingStatus.CANCELLED && 
                ((request.StartTime >= b.StartTime && request.StartTime < b.EndTime) || 
                 (request.EndTime > b.StartTime && request.EndTime <= b.EndTime) ||   
                 (request.StartTime <= b.StartTime && request.EndTime >= b.EndTime)));  

            
            if (isCourtOccupied)
            {
                _logger.LogWarning("Pembuatan booking ditolak: Lapangan '{CourtName}' sudah dipesan pada tanggal {Date} jam {Start}-{End}",
                    court.CourtName, request.BookingDate.ToString("yyyy-MM-dd"), request.StartTime, request.EndTime);
                throw new CourtNotAvailableException(court.CourtName ?? request.CourtId.ToString());
            }

            decimal baseAmount = 0;
            TimeSpan currentHour = request.StartTime;

            bool isWeekend = request.BookingDate.DayOfWeek == DayOfWeek.Saturday ||
                             request.BookingDate.DayOfWeek == DayOfWeek.Sunday;

            for (int i = 0; i < total_hours; i++)
            {
                decimal hourlyRate = court.HourlyRate;

                if (currentHour >= TimeSpan.FromHours(8) && currentHour < TimeSpan.FromHours(17))
                {
                    hourlyRate = court.HourlyRate;
                }
                else if (currentHour >= TimeSpan.FromHours(17) && currentHour < TimeSpan.FromHours(22))
                {
                    hourlyRate = court.HourlyRate + (court.HourlyRate * 0.20m);
                }
                else
                {
                    throw new Exception("Jam sewa diluar jam operasional");
                }

                if (isWeekend)
                {
                    hourlyRate += hourlyRate * 0.20m;
                }

                baseAmount += hourlyRate;
                currentHour = currentHour.Add(TimeSpan.FromHours(1));
            }

            if (total_hours > 3)
            {
                baseAmount -= 50000;
                if (baseAmount < 0) baseAmount = 0;
            }

            decimal totalAmount = baseAmount;
            string bookingCode = "BK-" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper();

            var booking = new Bookings
            {
                AdminId = currentAdminId,
                CourtId = request.CourtId,
                CustomerName = request.CustomerName,
                BookingDate = request.BookingDate,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                BookingCode = bookingCode,
                TotalHours = total_hours,
                BaseAmount = baseAmount,
                TotalAmount = baseAmount,
                Status = BookingStatus.ACTIVE,
                CreatedAt = DateTime.UtcNow
            };

            await _bookingRepository.AddAsync(booking);
            await _bookingRepository.SaveChangesAsync();

            if (request.AddOns != null && request.AddOns.Any())
            {
                _logger.LogInformation("Memproses {Count} item perlengkapan tambahan untuk Kode Booking: {BookingCode}", request.AddOns.Count, bookingCode);
                foreach (var addOnReq in request.AddOns)
                {
                    var equipment = await _equipmentRepository.GetByIdAsync(addOnReq.EquipmentAddOnsId);
                    if (equipment == null)
                    {
                        _logger.LogError("Gagal memproses add-on: Item ID {EquipmentId} tidak ditemukan", addOnReq.EquipmentAddOnsId);
                        throw new Exception($"Item perlengkapan dengan ID {addOnReq.EquipmentAddOnsId} tidak ditemukan");
                    }
                    if (equipment.Stock < addOnReq.Quantity)
                    {
                        _logger.LogWarning("Gagal memproses add-on: Stok '{ItemName}' tidak mencukupi. Sisa stok: {Stock}, Diminta: {Qty}",
                            equipment.ItemName, equipment.Stock, addOnReq.Quantity);
                        throw new Exception($"Stok item {equipment.ItemName} tidak mencukupi");
                    }

                    equipment.Stock -= addOnReq.Quantity;
                    _equipmentRepository.Update(equipment);

                    decimal subTotal = equipment.RentalFee * addOnReq.Quantity;
                    totalAmount += subTotal;

                    var bookingAddOn = new BookingAddOns
                    {
                        BookingId = booking.Id,
                        EquipmentId = equipment.Id,
                        Quantity = addOnReq.Quantity,
                        UnitPrice = equipment.RentalFee,
                        SubTotal = subTotal
                    };

                    await _bookingAddOnsRepository.AddAsync(bookingAddOn);
                }
            }

            booking.TotalAmount = totalAmount;
            _bookingRepository.Update(booking);
            await _bookingRepository.SaveChangesAsync();

            _logger.LogInformation("Transaksi BERHASIL dibuat. Kode Booking: {BookingCode}, Total Bayar: {TotalAmount}", bookingCode, totalAmount);

            return ToListResponse(booking);
        }

        public async Task<UpdateBookingStatusResponse?> UpdateStatusAsync(long id, BookingStatus newStatus)
        {
            var booking = await _bookingRepository.GetByIdAsync(id);
            if (booking == null) return null;

            booking.Status = newStatus;
            _bookingRepository.Update(booking);
            await _bookingRepository.SaveChangesAsync();

            return new UpdateBookingStatusResponse
            {
                BookingId = booking.Id,
                Status = "Berhasil memperbarui status terkini"
            };
        }

        public async Task<bool> CancelAsync(long id)
        {
            var booking = await _bookingRepository.GetByIdAsync(id);
            if (booking == null) return false;

            booking.Status = BookingStatus.CANCELLED;
            _bookingRepository.Update(booking);
            await _bookingRepository.SaveChangesAsync();

            return true;
        }

        public BookingListResponse ToListResponse(Bookings bookings)
        {
            return new BookingListResponse
            {
                Id = bookings.Id,
                BookingCode = bookings.BookingCode,
                AdminId = bookings.AdminId,
                CourtId = bookings.CourtId,
                CustomerName = bookings.CustomerName,
                BookingDate = bookings.BookingDate,
                StartTime = bookings.StartTime,
                EndTime = bookings.EndTime,
                TotalHours = bookings.TotalHours,
                BaseAmount = bookings.BaseAmount,
                TotalAmount = bookings.TotalAmount,
                Status = bookings.Status,
                CreatedAt = bookings.CreatedAt
            };
        }

        public BookingDetailResponse ToDetailResponse(Bookings bookings)
        {
            return new BookingDetailResponse
            {
                Id = bookings.Id,
                BookingCode = bookings.BookingCode,
                AdminId = bookings.AdminId,
                CourtId = bookings.CourtId,
                CustomerName = bookings.CustomerName,
                BookingDate = bookings.BookingDate,
                StartTime = bookings.StartTime,
                EndTime = bookings.EndTime,
                TotalHours = bookings.TotalHours,
                BaseAmount = bookings.BaseAmount,
                TotalAmount = bookings.TotalAmount,
                Status = bookings.Status,
                CreatedAt = bookings.CreatedAt,
                BookingAddOns = bookings.BookingAddOns?.Select(ba => new BookingAddOnResponse
                {
                    Id = ba.Id,
                    BookingId = ba.BookingId,
                    EquipmentAddOnsId = ba.EquipmentId,
                    Quantity = ba.Quantity,
                    UnitPrice = ba.UnitPrice,
                    Subtotal = ba.SubTotal,
                    EquipmentName = ba.Equipment?.ItemName ?? string.Empty
                }).ToList() ?? new List<BookingAddOnResponse>()
            };
        }
    }
}