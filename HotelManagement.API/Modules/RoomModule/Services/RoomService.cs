using HotelManagement.API.Exceptions;
using HotelManagement.API.Modules.RoomModule.DTOs;
using HotelManagement.API.Modules.RoomModule.Repositories;

namespace HotelManagement.API.Modules.RoomModule.Services;

public class RoomService(IRoomRepository roomRepository) : IRoomService
{
    private readonly IRoomRepository _roomRepository = roomRepository;
    
    public async Task<RoomDetailsDto> GetRoomDetailsByIdAsync(int id)
    {
        var room = await _roomRepository.GetByIdAsync(id) ??
                   throw new NotFoundException("No such room exists.");

        return new RoomDetailsDto
        {
            RoomNumber = room.RoomNumber ?? 0,
            RoomTypeId = room.RoomTypeId ?? 0,
            IsAvailable = room.IsAvailable ?? false,
        };
    }
}