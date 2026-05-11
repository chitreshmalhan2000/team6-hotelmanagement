namespace HotelManagement.API.Modules.ReservationModule.Exceptions;

public class ReservationNotFoundException : Exception
{
    public ReservationNotFoundException() : base("Reservation not found") { }
    public ReservationNotFoundException(int reservationId) : base($"Reservation with id {reservationId} not found") {}
    public ReservationNotFoundException(string message) : base(message) { }
    public ReservationNotFoundException(string message, Exception inner) : base(message, inner) { }
}