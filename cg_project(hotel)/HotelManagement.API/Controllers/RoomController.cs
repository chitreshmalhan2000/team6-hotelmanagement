using HotelManagement.API.Common;
using HotelManagement.API.DTOs;
using HotelManagement.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.API.Controllers;

[ApiController]
[Route("api/rooms")]
public class RoomController(IRoomService roomService) : ControllerBase
{
    [HttpGet]
 
    public async Task<IActionResult> GetAll() =>
        Ok(new ApiResponse<object> { Success = true, Message = "Rooms fetched.", Data = await roomService.GetAllAsync() });

    [HttpGet("{id:int}")]
  
    public async Task<IActionResult> GetById(int id)
    {
        var item = await roomService.GetByIdAsync(id);
        return item is null
            ? NotFound(new ApiResponse<object> { Success = false, Message = "Room not found." })
            : Ok(new ApiResponse<object> { Success = true, Message = "Room fetched.", Data = item });
    }

    [HttpPost]
   
    public async Task<IActionResult> Create([FromBody] RoomCreateDto dto)
    {
        var created = await roomService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.RoomId },
            new ApiResponse<object> { Success = true, Message = "Room created.", Data = created });
    }

    [HttpPut("{id:int}")]
   
    public async Task<IActionResult> Update(int id, [FromBody] RoomUpdateDto dto) =>
        await roomService.UpdateAsync(id, dto)
            ? Ok(new ApiResponse<object> { Success = true, Message = "Room updated." })
            : NotFound(new ApiResponse<object> { Success = false, Message = "Room not found." });

    [HttpDelete("{id:int}")]
  
    public async Task<IActionResult> Delete(int id) =>
        await roomService.DeleteAsync(id)
            ? Ok(new ApiResponse<object> { Success = true, Message = "Room deleted." })
            : NotFound(new ApiResponse<object> { Success = false, Message = "Room not found." });

    [HttpGet("available")]

    public async Task<IActionResult> GetAvailable() =>
        Ok(new ApiResponse<object> { Success = true, Message = "Available rooms fetched.", Data = await roomService.GetAvailableRoomsAsync() });

    [HttpGet("by-type/{roomTypeId:int}")]
 
    public async Task<IActionResult> GetByType(int roomTypeId) =>
        Ok(new ApiResponse<object> { Success = true, Message = "Rooms by type fetched.", Data = await roomService.GetByRoomTypeAsync(roomTypeId) });

    [HttpGet("{id:int}/amenities")]
  
    public async Task<IActionResult> GetAmenities(int id) =>
        Ok(new ApiResponse<object> { Success = true, Message = "Room amenities fetched.", Data = await roomService.GetAmenitiesByRoomIdAsync(id) });

    [HttpPut("{id:int}/availability")]
   
    public async Task<IActionResult> UpdateAvailability(int id, [FromBody] RoomAvailabilityUpdateDto dto) =>
        await roomService.UpdateAvailabilityAsync(id, dto)
            ? Ok(new ApiResponse<object> { Success = true, Message = "Room availability updated." })
            : NotFound(new ApiResponse<object> { Success = false, Message = "Room not found." });

    [HttpGet("{id:int}/reservations")]
   
    public async Task<IActionResult> GetReservations(int id) =>
        Ok(new ApiResponse<object> { Success = true, Message = "Room reservations fetched.", Data = await roomService.GetReservationsByRoomIdAsync(id) });
}
