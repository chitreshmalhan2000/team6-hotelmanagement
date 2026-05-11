using HotelManagement.API.Exceptions;
using HotelManagement.API.Modules.RoomTypeModule.DTOs;
using HotelManagement.API.Modules.RoomTypeModule.Repositories;

namespace HotelManagement.API.Modules.RoomTypeModule.Services;

public class RoomTypeService(IRoomTypeRepository roomTypeRepository) : IRoomTypeService
{
    private readonly IRoomTypeRepository _roomTypeRepository = roomTypeRepository;
    
    public async Task<RoomTypeDetailsDto> GetRoomTypeDetailsByIdAsync(int id)
    {
        var roomType = await _roomTypeRepository.GetRoomTypeByIdAsync(id) ??
                       throw new NotFoundException("Room Type not found.");

        return new RoomTypeDetailsDto
        {
            TypeName = roomType.TypeName ?? string.Empty,
            Description = roomType.Description ?? string.Empty,
            MaxOccupancy = roomType.MaxOccupancy ?? -1,
            PricePerNight = roomType.PricePerNight ?? -1,
        };
    }
}