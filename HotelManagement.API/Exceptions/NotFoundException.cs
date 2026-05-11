namespace HotelManagement.API.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException() : base("Not found exception raised.") { }
    public NotFoundException(string message) : base(message) { }
    public NotFoundException(string message, Exception innerException) : base(message, innerException) { }
}