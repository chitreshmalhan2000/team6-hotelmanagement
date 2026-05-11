using HotelManagement.API.Modules.RoomTypeModule.DTOs;
using HotelManagement.Common.Data;
using HotelManagement.Common.Models;

namespace HotelManagement.API.Modules.RoomTypeModule.Repositories;

public class RoomTypeRepository(HotelDbContext dbContext) : IRoomTypeRepository
{
    private readonly HotelDbContext _dbContext = dbContext;

    public async Task<RoomType?> GetRoomTypeByIdAsync(int id) =>
        await _dbContext.RoomTypes.FindAsync(id);
}