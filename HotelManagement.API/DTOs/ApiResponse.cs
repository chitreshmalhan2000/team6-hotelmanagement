namespace HotelManagement.API.DTOs;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public int StatusCode { get; set; }
    public string? Message { get; set; } = null;
    public T? Data { get; set; }
    public List<string>? Errors { get; set; } = null;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}