using HotelManagement.API.Modules.RoomModule.DTOs;

namespace HotelManagement.API.Modules.RoomModule.Services;

public interface IRoomService
{
    Task<RoomDetailsDto> GetRoomDetailsByIdAsync(int id);
}