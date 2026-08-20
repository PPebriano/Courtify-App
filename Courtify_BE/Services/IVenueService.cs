using CourtifyBE.DTOs;
using CourtifyBE.Models;

namespace CourtifyBE.Services
{
    public interface IVenueService
    {
        Task <IEnumerable<VenueResponse>> GetAllAsync(string? search, int page, int limit);
        //Task<List<Venues>> GetAllAsync(string? status, DateTime? date);
        Task<VenueDetailResponse?> GetByIdAsync(long id);
        //VenueDetailResponse ToDetailResponse(Venues venue);


    }
}
