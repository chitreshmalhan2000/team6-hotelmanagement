namespace HotelManagement.API.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException() : base("Bad Request exception raised.") { }
    public  BadRequestException(string message) : base(message) { }
    public  BadRequestException(string message, Exception innerException) : base(message, innerException) { }
}