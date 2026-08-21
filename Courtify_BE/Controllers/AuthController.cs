using CourtifyBE.DTOs;
using CourtifyBE.Services;
using Microsoft.AspNetCore.Mvc;

namespace CourtifyBE.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            LoginResponse? result = await _authService.LoginAsync(request);
            if (result == null) return Unauthorized(new { status = "error", message = "Email atau Password Salah" });

            return Ok(result);
        }
    }
}
