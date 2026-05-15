using HotelManagement.API.Modules.RoomModule.Controllers;
using HotelManagement.API.Modules.RoomModule.DTOs;
using HotelManagement.API.Modules.RoomModule.Services;
using HotelManagement.API.Modules.AmenityModule.DTOs;
using HotelManagement.API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Tests;

public class RoomControllerTests
{
    [Fact]
    public async Task GetAll_ReturnsOkWithRooms()
    {
        var controller = new RoomController(new FakeRoomService
        {
            Rooms = [new RoomDto { RoomId = 1, RoomNumber = 101, RoomTypeId = 2, IsAvailable = true }]
        });

        var result = await controller.GetAll();

        var ok = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse<object>>(ok.Value);
        Assert.True(response.Success);
    }

    [Fact]
    public async Task GetById_WhenRoomExists_ReturnsOk()
    {
        var controller = new RoomController(new FakeRoomService
        {
            Room = new RoomDto { RoomId = 1, RoomNumber = 101, RoomTypeId = 2 }
        });

        var result = await controller.GetById(1);

        var ok = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse<object>>(ok.Value);
        Assert.Equal("Room fetched.", response.Message);
    }

    [Fact]
    public async Task Create_ReturnsCreatedAtAction()
    {
        var controller = new RoomController(new FakeRoomService
        {
            CreatedRoom = new RoomDto { RoomId = 5, RoomNumber = 205, RoomTypeId = 3 }
        });

        var result = await controller.Create(new RoomCreateDto { RoomNumber = 205, RoomTypeId = 3 });

        var created = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(nameof(RoomController.GetById), created.ActionName);
    }

    [Fact]
    public async Task UpdateAvailability_WhenRoomExists_ReturnsOk()
    {
        var controller = new RoomController(new FakeRoomService { UpdateAvailabilityResult = true });

        var result = await controller.UpdateAvailability(1, new RoomAvailabilityUpdateDto { IsAvailable = false });

        var ok = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse<object>>(ok.Value);
        Assert.Equal("Room availability updated.", response.Message);
    }

    [Fact]
    public async Task GetById_WhenRoomMissing_ReturnsNotFound()
    {
        var controller = new RoomController(new FakeRoomService());

        var result = await controller.GetById(404);

        var notFound = Assert.IsType<NotFoundObjectResult>(result);
        var response = Assert.IsType<ApiResponse<object>>(notFound.Value);
        Assert.False(response.Success);
    }

    [Fact]
    public async Task Update_WhenRoomMissing_ReturnsNotFound()
    {
        var controller = new RoomController(new FakeRoomService { UpdateResult = false });

        var result = await controller.Update(404, new RoomUpdateDto { RoomNumber = 201, RoomTypeId = 2 });

        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task Delete_WhenRoomMissing_ReturnsNotFound()
    {
        var controller = new RoomController(new FakeRoomService { DeleteResult = false });

        var result = await controller.Delete(404);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task UpdateAvailability_WhenRoomMissing_ReturnsNotFound()
    {
        var controller = new RoomController(new FakeRoomService { UpdateAvailabilityResult = false });

        var result = await controller.UpdateAvailability(404, new RoomAvailabilityUpdateDto { IsAvailable = true });

        Assert.IsType<NotFoundObjectResult>(result);
    }

    private sealed class FakeRoomService : IRoomService
    {
        public List<RoomDto> Rooms { get; set; } = [];
        public RoomDto? Room { get; set; }
        public RoomDto? CreatedRoom { get; set; }
        public bool UpdateResult { get; set; }
        public bool UpdateAvailabilityResult { get; set; }
        public bool DeleteResult { get; set; }

        public Task<List<RoomDto>> GetAllAsync() => Task.FromResult(Rooms);
        public Task<RoomDto?> GetByIdAsync(int id) => Task.FromResult(Room);
        public Task<List<RoomDto>> GetAvailableRoomsAsync() => Task.FromResult(Rooms);
        public Task<List<RoomDto>> GetByRoomTypeAsync(int roomTypeId) => Task.FromResult(Rooms);
        public Task<RoomDto?> GetRoomDetailsByIdAsync(int id) => Task.FromResult(Room);
        public Task<List<AmenityDto>> GetAmenitiesByRoomIdAsync(int id) => Task.FromResult(new List<AmenityDto>());
        public Task<List<object>> GetReservationsByRoomIdAsync(int id) => Task.FromResult(new List<object>());
        public Task<RoomDto> CreateAsync(RoomCreateDto dto) => Task.FromResult(CreatedRoom ?? new RoomDto { RoomId = 1, RoomNumber = dto.RoomNumber, RoomTypeId = dto.RoomTypeId });
        public Task<bool> UpdateAsync(int id, RoomUpdateDto dto) => Task.FromResult(UpdateResult);
        public Task<bool> UpdateAvailabilityAsync(int id, RoomAvailabilityUpdateDto dto) => Task.FromResult(UpdateAvailabilityResult);
        public Task<bool> DeleteAsync(int id) => Task.FromResult(DeleteResult);
    }
}
