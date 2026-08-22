using CourtifyBE.DTOs;
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

        public BookingService(
            IRepository<Bookings> bookingRepository, 
            IRepository<Courts> courtRepository, 
            IRepository<EquipmentAddOns> equipmentRepository, 
            IRepository<BookingAddOns> bookingAddOnsRepository)
        {
            _bookingRepository = bookingRepository;
            _courtRepository = courtRepository;
            _equipmentRepository = equipmentRepository;
            _bookingAddOnsRepository = bookingAddOnsRepository;
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

        public async Task<BookingDetailResponse> CreateFullTransactionAsync(CreateBookingRequest request, long adminId)
        {
            // Validasi 
            var court = await _courtRepository.GetByIdAsync(request.CourtId);
            if (court == null) throw new Exception("Lapangan tidak ada");

            int total_hours = (int)(request.EndTime - request.StartTime).TotalHours;
            if (total_hours <= 0) throw new Exception("Waktu sewa tidak valid");

            // Biaya dasar lapangan
            decimal baseAmount = 0;
            TimeSpan currentHour = request.StartTime;

            bool isWeekend = request.BookingDate.DayOfWeek == DayOfWeek.Saturday ||
                request.BookingDate.DayOfWeek == DayOfWeek.Sunday;

            for (int i = 0; i < total_hours; i++)
            {
                decimal hourlyRate = 0;
                if (currentHour >= TimeSpan.FromHours(8) && currentHour < TimeSpan.FromHours(17))
                {
                    hourlyRate = 100000;
                }
                else if (currentHour >= TimeSpan.FromHours(17) && currentHour < TimeSpan.FromHours(22))
                {
                    hourlyRate = 150000;
                }
                else
                {
                    throw new Exception($"Jam sewa diluar jam operasional");
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

            // Generate KODE BOOKING
            string bookingCode = "BK-" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper();

            var booking = new Bookings
            {
                AdminId = adminId,
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

            if (request.AddOns != null & request.AddOns.Any())
            {
                foreach (var addOnReq in request.AddOns)
                {
                    var equipment = await _equipmentRepository.GetByIdAsync(addOnReq.EquipmentAddOnsId);
                    if (equipment == null)
                    {
                        throw new Exception($"Item perlengkapan dengan ID {addOnReq.EquipmentAddOnsId} tidak ditemukan");
                    }
                    if (equipment.Stock < addOnReq.Quantity)
                    {
                        throw new Exception($"Stoke item {equipment.ItemName} tidak mencukupi");
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

            return new BookingDetailResponse
            {
                Id = booking.Id,
                AdminId = booking.AdminId,
                CourtId = booking.CourtId,
                CustomerName = booking.CustomerName,
                BookingCode = bookingCode,
                BookingDate = booking.BookingDate,
                StartTime = booking.StartTime,
                EndTime = booking.EndTime,
                TotalHours = booking.TotalHours,
                BaseAmount = booking.BaseAmount,
                TotalAmount = booking.TotalAmount,
                Status = booking.Status,
                CreatedAt = booking.CreatedAt
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
                Status = BookingStatus.ACTIVE,
                CreatedAt = bookings.CreatedAt

            };
        }


    }
}
