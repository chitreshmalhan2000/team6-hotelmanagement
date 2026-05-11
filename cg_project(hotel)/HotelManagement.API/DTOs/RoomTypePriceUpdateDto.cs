using System.ComponentModel.DataAnnotations;

namespace HotelManagement.API.DTOs;

public class RoomTypePriceUpdateDto
{
    [Required]
    [Range(0.01, 100000)]
    public decimal PricePerNight { get; set; }
}
