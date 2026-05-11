using HotelManagement.API.Filters;
using HotelManagement.API.Modules.AuthModule.DTOs;
using HotelManagement.API.Modules.AuthModule.Services;
using HotelManagement.Common.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.API.Modules.AuthModule.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(UserManager<ApplicationUser> userManager, IAuthTokenService tokenService) : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IAuthTokenService _tokenService = tokenService;
    
    [HttpPost("login")]
    [ServiceFilter(typeof(ValidationFilters<LoginRequestDto>))]
    public async Task<IActionResult> Login(LoginRequestDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email)
            ?? throw new InvalidOperationException($"User with email '{dto.Email}' does not exist.");
        
        if (!await _userManager.CheckPasswordAsync(user, dto.Password))
            return Unauthorized("Invalid password.");
        
        var roles = await _userManager.GetRolesAsync(user);
        var token = _tokenService.GetToken(user, roles);
        
        return Ok(new { token });
    }

    [HttpPost("register")]
    [ServiceFilter(typeof(ValidationFilters<RegisterRequestDto>))]
    public async Task<IActionResult> Register(RegisterRequestDto dto)
    {
        var user = new ApplicationUser
        {
            UserName = dto.Email,
            Email = dto.Email,
        };

        var result = await _userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded) return BadRequest();
                     
        await _userManager.AddToRoleAsync(user, dto.Role);
        return CreatedAtAction(nameof(Register), new { userId = user.Id, role = dto.Role },  null);
    }
}
