namespace HotelManagement.API.DTOs;

public class ReviewDto
{
    public int ReviewId { get; set; }
    public int ReservationId { get; set; }
    public int Rating { get; set; }
    public string? Comment { get; set; }
    public DateOnly? ReviewDate { get; set; }
    public DateTime? CreatedAt { get; set; }
}
