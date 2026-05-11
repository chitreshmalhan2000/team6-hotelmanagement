using HotelManagement.API.Exceptions;
using HotelManagement.API.Modules.HotelModule.DTOs;
using HotelManagement.API.Modules.HotelModule.Repositories;

namespace HotelManagement.API.Modules.HotelModule.Services;

public class HotelService(IHotelRepository hotelRepository) : IHotelService
{
    private readonly IHotelRepository _hotelRepository = hotelRepository;
    
    public async Task<HotelDetailsDto> GetHotelDetailsByIdAsync(int hotelId)
    {
        var hotel = await _hotelRepository.GetByIdAsync(hotelId) ??
                    throw new NotFoundException("Hotel not found");

        return new HotelDetailsDto
        {
            Name = hotel.Name ?? string.Empty,
            Description = hotel.Description ?? string.Empty,
            Location = hotel.Location ?? string.Empty,
        };
    }

    public async Task<int> GetHotelIdByRoomIdAsync(int roomId)
    {
        var hotelId = await _hotelRepository.GetIdByRoomIdAsync(roomId) ??
                      throw new NotFoundException("Hotel not found");

        return hotelId;
    }
}
