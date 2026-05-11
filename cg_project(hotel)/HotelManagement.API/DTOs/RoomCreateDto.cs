using System.ComponentModel.DataAnnotations;

namespace HotelManagement.API.DTOs;

public class RoomCreateDto
{
    [Required]
    [Range(1, 9999)]
    public int RoomNumber { get; set; }

    [Required]
    public int RoomTypeId { get; set; }

    public bool IsAvailable { get; set; } = true;
}
