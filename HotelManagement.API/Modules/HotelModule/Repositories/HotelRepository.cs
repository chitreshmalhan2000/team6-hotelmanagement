using HotelManagement.Common.Data;
using HotelManagement.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.API.Modules.HotelModule.Repositories;

public class HotelRepository(HotelDbContext dbContext) : IHotelRepository
{
    private readonly HotelDbContext _dbContext = dbContext;

    public async Task<Hotel?> GetByIdAsync(int hotelId) =>
        await _dbContext.Hotels.FindAsync(hotelId);

    public async Task<int?> GetIdByRoomIdAsync(int roomId)
    {
        var hotel = await _dbContext.Rooms
            .Where(r => r.RoomId == roomId)
            .SelectMany(r => r.Amenities)
            .SelectMany(a => a.Hotels)
            .Distinct()
            .FirstOrDefaultAsync();

        return hotel?.HotelId;
    }

    public async Task<IEnumerable<Hotel>> GetAllAsync() =>
        await _dbContext.Hotels.ToListAsync();

    public async Task<Hotel> CreateAsync(Hotel hotel)
    {
        _dbContext.Hotels.Add(hotel);
        await _dbContext.SaveChangesAsync();
        return hotel;
    }

    public async Task<Hotel> UpdateAsync(Hotel hotel)
    {
        _dbContext.Hotels.Update(hotel);
        await _dbContext.SaveChangesAsync();
        return hotel;
    }

    public async Task DeleteAsync(Hotel hotel)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Hotel>> SearchByLocationAsync(string location) =>
        await _dbContext.Hotels
            .Where(hotel => hotel.Location != null && hotel.Location.ToLower().Contains(location.ToLower()))
            .ToListAsync();

    public async Task<IEnumerable<Room>> GetRoomsByHotelIdAsync(int hotelId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Room>> GetAvailableRoomsByHotelIdAsync(int hotelId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Reservation>> GetReservationsByHotelIdAsync(int hotelId)
    {
        throw new NotImplementedException();
    }
}
