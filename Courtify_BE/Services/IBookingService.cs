using CourtifyBE.DTOs;
using CourtifyBE.Models;

namespace CourtifyBE.Services
{
    public interface IBookingService
    {
        Task<Bookings?> GetByIdAsync(long id);
        Task<List<Bookings>> GetAllAsync(string? status, DateTime? createdAt);
        BookingDetailResponse ToDetailResponse(Bookings? bookings);
        //Task<BookingDetailResponse> CreateFullTransactionAsync(CreateBookingRequest request, long currentAdminId);

    }
}
