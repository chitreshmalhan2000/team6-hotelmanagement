namespace HotelManagement.API.DTOs;

public class RoomTypeDto
{
    public int RoomTypeId { get; set; }
    public string TypeName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int? MaxOccupancy { get; set; }
    public decimal? PricePerNight { get; set; }
}
