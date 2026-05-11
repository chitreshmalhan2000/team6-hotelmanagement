using HotelManagement.API.Modules.AmenityModule.DTOs;

namespace HotelManagement.API.Modules.AmenityModule.Services;

public interface IAmenityService
{
    Task<AmenityDto> GetAmenityByIdAsync(int amenityId);
    Task<List<AmenityDto>> GetAmenitiesByRoomIdAsync(int roomId);
}