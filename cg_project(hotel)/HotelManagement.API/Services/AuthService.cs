using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HotelManagement.API.Common;
using HotelManagement.API.DTOs;
using HotelManagement.API.Repositories;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace HotelManagement.API.Services;

public class AuthService(IUserRepository userRepository, IOptions<JwtSettings> jwtOptions) : IAuthService
{
    public async Task<AuthResponseDto?> LoginAsync(LoginRequestDto request)
    {
        var user = await userRepository.GetByUsernameAsync(request.Username);
        if (user is null || user.PasswordHash != request.Password) return null;

        var jwt = jwtOptions.Value;
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.UtcNow.AddMinutes(jwt.ExpirationMinutes);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role?.Name ?? "User")
        };

        var token = new JwtSecurityToken(jwt.Issuer, jwt.Audience, claims, expires: expires, signingCredentials: creds);
        return new AuthResponseDto
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Username = user.Username,
            Role = user.Role?.Name ?? "User",
            ExpiresAtUtc = expires
        };
    }
}
