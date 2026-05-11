using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HotelManagement.API.Exceptions;
using HotelManagement.Common.Models;
using Microsoft.IdentityModel.Tokens;

namespace HotelManagement.API.Modules.AuthModule.Services;

public class AuthTokenService(IConfiguration configuration) : IAuthTokenService
{
    private readonly IConfiguration _configuration = configuration;
    
    public string GetToken(ApplicationUser user, IList<string> roles)
    {
        var email = user.Email ??
                    throw new NotFoundException("Email cannot be null.");
        
        List<Claim> claims = [
            new Claim(ClaimTypes.Email, email), 
            new Claim(ClaimTypes.NameIdentifier, user.Id)];
        
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
        
        var jwtSecretKey = _configuration["Jwt:Key"] ??
                              throw new NotFoundException("Secret key not found.");
        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.Now.AddHours(2),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey)),
                SecurityAlgorithms.HmacSha256));
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
