using System.ComponentModel.DataAnnotations;

namespace HotelManagement.MVC.ViewModels;

public class AmenityViewModel
{
    public int AmenityId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}

public class CreateAmenityViewModel
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Description { get; set; }
}
