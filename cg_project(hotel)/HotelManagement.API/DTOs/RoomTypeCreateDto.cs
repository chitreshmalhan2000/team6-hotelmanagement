using System.ComponentModel.DataAnnotations;

namespace HotelManagement.API.DTOs;

public class RoomTypeCreateDto
{
    [Required]
    [MaxLength(255)]
    public string TypeName { get; set; } = string.Empty;

    [MaxLength(2000)]
    public string? Description { get; set; }

    [Range(1, 20)]
    public int? MaxOccupancy { get; set; }

    [Range(0.01, 100000)]
    public decimal? PricePerNight { get; set; }
}
