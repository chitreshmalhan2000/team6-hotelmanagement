namespace HotelManagement.API.Modules.RoomModule.DTOs;

public class RoomDetailsDto
{
    public int RoomNumber { get; set; }
    public int RoomTypeId { get; set; }
    public bool IsAvailable { get; set; }
}