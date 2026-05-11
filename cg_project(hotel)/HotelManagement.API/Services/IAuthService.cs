using HotelManagement.API.DTOs;

namespace HotelManagement.API.Services;

public interface IAuthService
{
    Task<AuthResponseDto?> LoginAsync(LoginRequestDto request);
}
