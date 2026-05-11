namespace HotelManagement.API.DTOs;

public class RoomDto
{
    public int RoomId { get; set; }
    public int? RoomNumber { get; set; }
    public int? RoomTypeId { get; set; }
    public string? RoomTypeName { get; set; }
    public bool? IsAvailable { get; set; }
}
