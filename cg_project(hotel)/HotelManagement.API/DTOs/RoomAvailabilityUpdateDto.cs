using System.ComponentModel.DataAnnotations;

namespace HotelManagement.API.DTOs;

public class RoomAvailabilityUpdateDto
{
    [Required]
    public bool IsAvailable { get; set; }
}
