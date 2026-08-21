using CourtifyBE.DTOs;
using CourtifyBE.Models;
using CourtifyBE.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CourtifyBE.Services
{
    public class AuthService : IAuthService
    {
        private readonly IRepository<Admin> _adminRepository;
        private readonly IConfiguration _config;

        public AuthService(IRepository<Admin> adminRepository, IConfiguration config)
        {
            _adminRepository = adminRepository;
            _config = config;
        }

        public async Task<LoginResponse?> LoginAsync(LoginRequest request)
        {
            List<Admin> admins = await _adminRepository.GetAllAsync();
            Admin? admin = admins.FirstOrDefault(a => a.Username == request.Username);

            if (admin == null || admin.Password != request.Password)
            {
                return null;
            }

            Claim[] claims = new Claim[]
            {
                new Claim("adminId", admin.Id.ToString()),
                new Claim("username", admin.Username)
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(
                    double.Parse(_config["Jwt:ExpireMinutes"]!)),
                signingCredentials: creds);

            return new LoginResponse
            {
                Status = "success",
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Admin = new UserSummary
                {
                    Id = admin.Id,
                    Nama = admin.Name
                }
            };

        }
    }
}
