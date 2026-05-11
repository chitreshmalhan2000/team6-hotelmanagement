using HotelManagement.API.Modules.HotelModule.DTOs;

namespace HotelManagement.API.Modules.HotelModule.Services;

public interface IHotelService
{
    Task<HotelDetailsDto> GetHotelDetailsByIdAsync(int hotelId);
    Task<int> GetHotelIdByRoomIdAsync(int roomId);
}