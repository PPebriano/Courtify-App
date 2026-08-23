using CourtifyBE.DTOs;
using CourtifyBE.Models;

namespace CourtifyBE.Services
{
    public interface IBookingService
    {
        Task<Bookings?> GetByIdAsync(long id);
        Task<List<Bookings>> GetAllAsync(string? status, DateTime? createdAt);
        Task<bool> CancelAsync(long id);
        BookingListResponse ToListResponse(Bookings bookings);
        BookingDetailResponse ToDetailResponse(Bookings bookings);
        Task<BookingListResponse> CreateFullTransactionAsync(CreateBookingRequest request, long currentAdminId);
        Task<UpdateBookingStatusResponse?> UpdateStatusAsync(long id, BookingStatus newStatus);
    }
}