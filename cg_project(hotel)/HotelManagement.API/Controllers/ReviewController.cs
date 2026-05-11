using HotelManagement.API.DTOs;
using HotelManagement.API.Common;
using HotelManagement.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.API.Controllers;

[ApiController]
[Route("api/reviews")]
public class ReviewController(IReviewService reviewService) : ControllerBase
{
    [HttpGet]
    
    public async Task<IActionResult> GetAll() =>
        Ok(new ApiResponse<object> { Success = true, Message = "Reviews fetched.", Data = await reviewService.GetAllAsync() });

    [HttpGet("{id:int}")]
    
    public async Task<IActionResult> GetById(int id)
    {
        var item = await reviewService.GetByIdAsync(id);
        return item is null
            ? NotFound(new ApiResponse<object> { Success = false, Message = "Review not found." })
            : Ok(new ApiResponse<object> { Success = true, Message = "Review fetched.", Data = item });
    }

    [HttpPost]
    
    public async Task<IActionResult> Create([FromBody] ReviewCreateDto dto)
    {
        var created = await reviewService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.ReviewId },
            new ApiResponse<object> { Success = true, Message = "Review created.", Data = created });
    }

    [HttpPut("{id:int}")]
   
    public async Task<IActionResult> Update(int id, [FromBody] ReviewUpdateDto dto) =>
        await reviewService.UpdateAsync(id, dto)
            ? Ok(new ApiResponse<object> { Success = true, Message = "Review updated." })
            : NotFound(new ApiResponse<object> { Success = false, Message = "Review not found." });

    [HttpDelete("{id:int}")]
   
    public async Task<IActionResult> Delete(int id) =>
        await reviewService.DeleteAsync(id)
            ? Ok(new ApiResponse<object> { Success = true, Message = "Review deleted." })
            : NotFound(new ApiResponse<object> { Success = false, Message = "Review not found." });

    [HttpGet("by-reservation/{reservationId:int}")]
  
    public async Task<IActionResult> ByReservation(int reservationId) =>
        Ok(new ApiResponse<object> { Success = true, Message = "Reviews by reservation fetched.", Data = await reviewService.GetByReservationIdAsync(reservationId) });

    [HttpGet("by-rating/{rating:int}")]
   
    public async Task<IActionResult> ByRating(int rating) =>
        Ok(new ApiResponse<object> { Success = true, Message = "Reviews by rating fetched.", Data = await reviewService.GetByRatingAsync(rating) });

    [HttpGet("latest")]
   
    public async Task<IActionResult> Latest() =>
        Ok(new ApiResponse<object> { Success = true, Message = "Latest reviews fetched.", Data = await reviewService.GetLatestAsync() });

    [HttpGet("average-rating")]
    
    public async Task<IActionResult> AverageRating() =>
        Ok(new ApiResponse<object> { Success = true, Message = "Average rating fetched.", Data = await reviewService.GetAverageRatingAsync() });

    [HttpGet("top-rated")]
  
    public async Task<IActionResult> TopRated() =>
        Ok(new ApiResponse<object> { Success = true, Message = "Top-rated reviews fetched.", Data = await reviewService.GetTopRatedAsync() });
}
