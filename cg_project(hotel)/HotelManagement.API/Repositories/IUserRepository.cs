using HotelManagement.API.Models;

namespace HotelManagement.API.Repositories;

public interface IUserRepository
{
    Task<User?> GetByUsernameAsync(string username);
}
