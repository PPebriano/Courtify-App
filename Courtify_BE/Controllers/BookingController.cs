using CourtifyBE.DTOs;
using CourtifyBE.Services;
using Microsoft.AspNetCore.Authorization;
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
            try
            {
                var bookings = await _bookingService.GetAllAsync(status, date);
                var response = bookings.Select(_bookingService.ToDetailResponse).ToList();

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Gagal mengambil data booking", error = ex.Message });
            }
        }

        [HttpGet("{id:long}")] // GET /api/booking/{id}
        public async Task<IActionResult> GetBooking(long id)
        {
            try
            {
                var booking = await _bookingService.GetByIdAsync(id);
                if (booking == null) return NotFound(new { message = $"Data booking dengan ID {id} tidak ditemukan" });

                return Ok(_bookingService.ToDetailResponse(booking));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Gagal mengambil detail booking", error = ex.Message });
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingRequest request)
        {
            try
            {
                var userIdClaim = User.FindFirst("adminId")?.Value;

                if (string.IsNullOrEmpty(userIdClaim))
                    return Unauthorized(new { message = "User tidak terautentikasi. Token tidak valid atau klaim adminId kosong." });

                long userId = long.Parse(userIdClaim);

                var result = await _bookingService.CreateFullTransactionAsync(request, userId);

                return CreatedAtAction(nameof(GetBooking), new { id = result.Id }, new
                {
                    status = "success",
                    message = "Input data booking berhasil"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", message = ex.Message });
            }
        }

        // PUT: /api/bookings/{id}/cancel
        [Authorize]
        [HttpDelete("{id:long}")]
        public async Task<IActionResult> CancelBooking(long id)
        {
            var success = await _bookingService.CancelAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
