using System.ComponentModel.DataAnnotations;

namespace HotelManagement.API.DTOs;

public class AmenityCreateDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Description { get; set; }
}
