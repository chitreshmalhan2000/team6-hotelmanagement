using System.Net;
using HotelManagement.API.Exceptions;

namespace HotelManagement.API.Middlewares;

public class GlobalExceptionMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        httpContext.Response.ContentType = "application/json";

        var statusCode = exception switch
        {
            NotFoundException => (int)HttpStatusCode.NotFound,
            BadRequestException => (int)HttpStatusCode.BadRequest,
            _ => (int)HttpStatusCode.InternalServerError
        };

        httpContext.Response.StatusCode = statusCode;
        var details = new List<string>();
        var message = exception.Message;

        while (exception.InnerException != null)
        {
            details.Add(exception.InnerException.Message);
            exception = exception.InnerException;
        }

        var response = new
        {
            Success = false,
            StatusCode = "500",
            Message = message,
            Details = details,
            TimeStamp = DateTime.UtcNow
        };
        
        return httpContext.Response.WriteAsJsonAsync(response);
    }
}