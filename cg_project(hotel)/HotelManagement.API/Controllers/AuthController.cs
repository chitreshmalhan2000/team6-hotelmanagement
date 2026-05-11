using HotelManagement.API.DTOs;
using HotelManagement.API.Common;
using HotelManagement.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        var result = await authService.LoginAsync(request);
        return result is null
            ? Unauthorized(new ApiResponse<object> { Success = false, Message = "Invalid credentials." })
            : Ok(new ApiResponse<object> { Success = true, Message = "Login successful.", Data = result });
    }
}
