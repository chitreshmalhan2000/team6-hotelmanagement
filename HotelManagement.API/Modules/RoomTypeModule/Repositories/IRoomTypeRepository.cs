using HotelManagement.API.Modules.RoomTypeModule.DTOs;
using HotelManagement.Common.Models;

namespace HotelManagement.API.Modules.RoomTypeModule.Repositories;

public interface IRoomTypeRepository
{
    Task<RoomType?> GetRoomTypeByIdAsync(int id);
}