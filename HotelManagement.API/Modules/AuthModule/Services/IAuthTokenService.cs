using HotelManagement.Common.Models;

namespace HotelManagement.API.Modules.AuthModule.Services;

public interface IAuthTokenService
{
    string GetToken(ApplicationUser user, IList<string> roles);
}