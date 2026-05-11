using HotelManagement.Common.Models;

namespace HotelManagement.API.Modules.HotelModule.Repositories;

public interface IHotelRepository
{
    Task<Hotel?> GetByIdAsync(int hotelId);
    Task<int?> GetIdByRoomIdAsync(int roomId);
    Task<IEnumerable<Hotel>> GetAllAsync();
    Task<Hotel> CreateAsync(Hotel hotel);
    Task<Hotel> UpdateAsync(Hotel hotel);
    Task DeleteAsync(Hotel hotel);
    Task<IEnumerable<Hotel>> SearchByLocationAsync(string location);
    Task<IEnumerable<Room>> GetRoomsByHotelIdAsync(int hotelId);
    Task<IEnumerable<Room>> GetAvailableRoomsByHotelIdAsync(int hotelId);
    Task<IEnumerable<Reservation>> GetReservationsByHotelIdAsync(int hotelId);
}