namespace HotelManagement.API.Modules.PaymentModule.DTOs;

public class PaymentDetailsDto
{
    public int ReservationId { get; set; }
    public decimal Amount { get; set; }
    public DateOnly PaymentDate { get; set; }
    public string PaymentStatus { get; set; } = string.Empty;
}