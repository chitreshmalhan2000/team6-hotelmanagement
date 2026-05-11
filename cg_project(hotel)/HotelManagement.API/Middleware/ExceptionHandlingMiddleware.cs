using System.Net;
using System.Text.Json;
using HotelManagement.API.Common;
using HotelManagement.API.Exceptions;

namespace HotelManagement.API.Middleware;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            var statusCode = ex switch
            {
                NotFoundException => HttpStatusCode.NotFound,
                BadRequestException => HttpStatusCode.BadRequest,
                ValidationException => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError
            };

            if (statusCode == HttpStatusCode.InternalServerError)
            {
                logger.LogError(ex, "Unhandled exception occurred.");
            }
            else
            {
                logger.LogWarning(ex, "Request failed with a handled exception.");
            }

            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";
            var message = statusCode == HttpStatusCode.InternalServerError ? "An unexpected error occurred." : ex.Message;
            var payload = new ApiResponse<object> { Success = false, Message = message };
            await context.Response.WriteAsync(JsonSerializer.Serialize(payload));
        }
    }
}
