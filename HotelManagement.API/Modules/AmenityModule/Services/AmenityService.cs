using HotelManagement.API.Modules.AmenityModule.DTOs;
using HotelManagement.API.Modules.AmenityModule.Repositories;

namespace HotelManagement.API.Modules.AmenityModule.Services;

public class AmenityService(IAmenityRepository amenityRepository) : IAmenityService
{
    private readonly IAmenityRepository _amenityRepository = amenityRepository;
    
    public async Task<AmenityDto> GetAmenityByIdAsync(int amenityId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<AmenityDto>> GetAmenitiesByRoomIdAsync(int roomId) =>
        (await _amenityRepository.GetAmenitiesByRoomIdAsync(roomId))
            .Select(a => new AmenityDto
            {
                Name = a.Name ?? string.Empty,
                Description = a.Description ?? string.Empty,
            }).ToList();
}