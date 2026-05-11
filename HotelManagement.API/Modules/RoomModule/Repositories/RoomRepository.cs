using HotelManagement.Common.Data;
using HotelManagement.Common.Models;

namespace HotelManagement.API.Modules.RoomModule.Repositories;

public class RoomRepository(HotelDbContext dbContext) : IRoomRepository
{
    private readonly HotelDbContext _dbContext = dbContext;

    public async Task<Room?> GetByIdAsync(int id) =>
        await _dbContext.Rooms.FindAsync(id);
}