using System.ComponentModel.DataAnnotations;

namespace HotelManagement.API.DTOs;

public class ReviewUpdateDto
{
    [Range(1, 5)]
    public int Rating { get; set; }

    [MaxLength(1000)]
    public string? Comment { get; set; }
}
