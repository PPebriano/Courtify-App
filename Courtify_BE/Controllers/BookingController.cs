using CourtifyBE.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourtifyBE.Controllers
{
    [ApiController]
    [Route("api/bookings")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBookings([FromQuery] string? status, [FromQuery] DateTime? date)
        {
            var bookings = await _bookingService.GetAllAsync(status, date);
            var response = bookings.Select(_bookingService.ToDetailResponse).ToList();
            return Ok(response);
        }
    }
}
