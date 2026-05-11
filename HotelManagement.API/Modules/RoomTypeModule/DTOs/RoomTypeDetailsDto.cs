namespace HotelManagement.API.Modules.RoomTypeModule.DTOs;

public class RoomTypeDetailsDto
{
    public string TypeName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int MaxOccupancy { get; set; }
    public decimal PricePerNight { get; set; }
}