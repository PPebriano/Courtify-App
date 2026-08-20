using CourtifyBE.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourtifyBE.Controllers
{
    [ApiController]
    [Route("api/venues")]
    public class VenueController : ControllerBase
    {
        private readonly IVenueService _venueService;

        public VenueController(IVenueService venueService)
        {
            _venueService = venueService;
        }

        [HttpGet] // GET /api/Venues?search=&page=&limit=
        public async Task<IActionResult> GetVenues(
            [FromQuery] string? search,
            [FromQuery] int page = 1,
            [FromQuery] int limit = 10)
        {
            var movies = await _venueService.GetAllAsync(search, page, limit);
            return Ok(movies);
            //throw new Exception("Test Middle");
        }

        [HttpGet("{id:long}")] // GET /api/bookings/{id}
        public async Task<IActionResult> GetVenue(long id)
        {
            var venue = await _venueService.GetByIdAsync(id);
            if (venue == null) return NotFound();
            
            return Ok(venue);
        }
    }
}
