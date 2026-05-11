using HotelManagement.API.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.API.Controllers;

[ApiController]
[Route("api/hotels")]
public class HotelController : ControllerBase
{
    [HttpGet]
    public IActionResult Get() => Ok(new ApiResponse<object> { Success = true, Message = "Hotel module scaffolded." });
}

[ApiController]
[Route("api/reservations")]
public class ReservationController : ControllerBase
{
    [HttpGet]
    public IActionResult Get() => Ok(new ApiResponse<object> { Success = true, Message = "Reservation module scaffolded." });
}

[ApiController]
[Route("api/payments")]
public class PaymentController : ControllerBase
{
    [HttpGet]
    public IActionResult Get() => Ok(new ApiResponse<object> { Success = true, Message = "Payment module scaffolded." });
}

[ApiController]
[Route("api/hotel-amenities")]

public class HotelAmenityController : ControllerBase
{
    [HttpGet]
    public IActionResult Get() => Ok(new ApiResponse<object> { Success = true, Message = "HotelAmenity module scaffolded." });
}

[ApiController]
[Route("api/room-amenities")]
public class RoomAmenityController : ControllerBase
{
    [HttpGet]
    public IActionResult Get() => Ok(new ApiResponse<object> { Success = true, Message = "RoomAmenity module scaffolded." });
}
