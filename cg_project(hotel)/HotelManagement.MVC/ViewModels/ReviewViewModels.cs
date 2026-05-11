using System.ComponentModel.DataAnnotations;

namespace HotelManagement.MVC.ViewModels;

public class ReviewViewModel
{
    public int ReviewId { get; set; }
    public int ReservationId { get; set; }
    public int Rating { get; set; }
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateReviewViewModel
{
    [Required]
    public int ReservationId { get; set; }

    [Range(1, 5)]
    public int Rating { get; set; }

    [MaxLength(1000)]
    public string? Comment { get; set; }
}
