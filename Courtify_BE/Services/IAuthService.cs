using CourtifyBE.DTOs;
using Microsoft.AspNetCore.Identity.Data;

namespace CourtifyBE.Services
{
    public interface IAuthService
    {
        Task<LoginResponse?> LoginAsync(DTOs.LoginRequest request);
    }
}
