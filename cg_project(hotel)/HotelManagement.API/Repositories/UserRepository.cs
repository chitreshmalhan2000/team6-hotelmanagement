using HotelManagement.API.Data;
using HotelManagement.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.API.Repositories;

public class UserRepository(HotelDbContext context) : IUserRepository
{
    public Task<User?> GetByUsernameAsync(string username) =>
        context.Users.Include(x => x.Role).FirstOrDefaultAsync(x => x.Username == username);
}
