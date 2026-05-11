using HotelManagement.API.Modules.RoomTypeModule.DTOs;

namespace HotelManagement.API.Modules.RoomTypeModule.Services;

public interface IRoomTypeService
{
    Task<RoomTypeDetailsDto> GetRoomTypeDetailsByIdAsync(int id);
}