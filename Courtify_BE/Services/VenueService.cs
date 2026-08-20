using CourtifyBE.DTOs;
using CourtifyBE.Models;
using CourtifyBE.Repositories;

namespace CourtifyBE.Services
{
    public class VenueService : IVenueService
    {
        private readonly IRepository<Venues> _venueRepository;

        public VenueService(IRepository<Venues> venueRepository)
        {
            _venueRepository = venueRepository;
        }

        public async Task<IEnumerable<VenueResponse>> GetAllAsync(string? search, int page, int limit)
        {
            var venue = await _venueRepository.GetAllAsync();

            var venues = venue.AsEnumerable();
            if (!string.IsNullOrEmpty(search))
            {
                venues = venues
                    .Where(m => m.NameVenue.Contains(search, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            var pagedQuery = venues
                .OrderBy(m => m.Id)
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToList();

            return pagedQuery.ConvertAll(v => new VenueResponse(v));
        }
        //public async Task<List<Venues>> GetAllAsync(string? nameVenue, DateTime? date)
        //{
        //    var venues = await _venueRepository.GetAllAsync();

        //    if (!string.IsNullOrEmpty(nameVenue))
        //    {
        //        venues = venues.Where(b => b.NameVenue.ToString() == nameVenue).ToList();
        //    }

        //    return venues;
        //}
        public async Task<VenueDetailResponse?> GetByIdAsync(long id)
        {
            var venue = await _venueRepository.GetByIdAsync(id, "Courts", "Courts.CourtCategory");
            if (venue == null) return null;

            return new VenueDetailResponse
            {
                VenueId = venue.Id, // Sesuaikan properti ID Anda (Id / VenueId)
                VenueName = venue.NameVenue,
                Address = venue.Address,
                PhoneNumber = venue.PhoneNumber,
                IsActive = venue.IsActive,

                Courts = venue.Courts != null
                    ? venue.Courts.Select(c => new CourtDto
                    {
                        CourtId = c.Id,
                        CourtName = c.CourtName,
                        HourlyRate = c.HourlyRate,
                        IsAvailable = c.IsAvailable,
                        CourtCategory = c.CourtCategory != null ? new CourtCategoryDto
                        {
                            CategoryName = c.CourtCategory.Category,
                            Description = c.CourtCategory.Description ?? string.Empty
                        } : null!
                    }).ToList()
                    : new List<CourtDto>()
            };
        }
    }
}
