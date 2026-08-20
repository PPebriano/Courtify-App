using CourtifyBE.DTOs;
using CourtifyBE.Models;
using CourtifyBE.Repositories;

namespace CourtifyBE.Services
{
    public class BookingService : IBookingService
    {
        private readonly IRepository<Bookings> _bookingRepository;

        public BookingService(
            IRepository<Bookings> bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<Bookings?> GetByIdAsync(long id)
        {
            return await _bookingRepository.GetByIdAsync(id, "Courts", "BookingAddOns", "PaymentReceipts");
        }

        public async Task<List<Bookings>> GetAllAsync(string? status, DateTime? createdAt)
        {
            var query = await _bookingRepository.GetAllAsync();
            var bookings = query.AsQueryable();

            if (!string.IsNullOrEmpty(status))
                bookings = bookings.Where(b => b.Status.Equals(status, StringComparison.OrdinalIgnoreCase));

            if (createdAt.HasValue)
                bookings = bookings.Where(b => b.CreatedAt.Date == createdAt.Value.Date);

            return bookings.ToList();
        }

       public BookingDetailResponse ToDetailResponse(Bookings bookings)
        {
            return new BookingDetailResponse
            {
                Id = bookings.Id,
                AdminId = bookings.AdminId,
                CourtId = bookings.CourtsId,
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


    }
}
