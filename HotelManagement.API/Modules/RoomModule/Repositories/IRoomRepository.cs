using HotelManagement.Common.Models;

namespace HotelManagement.API.Modules.RoomModule.Repositories;

public interface IRoomRepository
{
    Task<Room?> GetByIdAsync(int id);
}