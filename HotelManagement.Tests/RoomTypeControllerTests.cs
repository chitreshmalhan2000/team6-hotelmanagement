using HotelManagement.API.Modules.RoomTypeModule.Controllers;
using HotelManagement.API.Modules.RoomTypeModule.DTOs;
using HotelManagement.API.Modules.RoomTypeModule.Services;
using HotelManagement.API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Tests;

public class RoomTypeControllerTests
{
    [Fact]
    public async Task GetAll_ReturnsOkWithRoomTypes()
    {
        var controller = new RoomTypeController(new FakeRoomTypeService
        {
            RoomTypes = [new RoomTypeDto { RoomTypeId = 1, TypeName = "Deluxe", MaxOccupancy = 2, PricePerNight = 1500 }]
        });

        var result = await controller.GetAll();

        var ok = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse<object>>(ok.Value);
        Assert.True(response.Success);
    }

    [Fact]
    public async Task GetById_WhenRoomTypeExists_ReturnsOk()
    {
        var controller = new RoomTypeController(new FakeRoomTypeService
        {
            RoomType = new RoomTypeDto { RoomTypeId = 1, TypeName = "Suite" }
        });

        var result = await controller.GetById(1);

        var ok = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse<object>>(ok.Value);
        Assert.Equal("Room type fetched.", response.Message);
    }

    [Fact]
    public async Task Create_ReturnsCreatedAtAction()
    {
        var controller = new RoomTypeController(new FakeRoomTypeService
        {
            CreatedRoomType = new RoomTypeDto { RoomTypeId = 3, TypeName = "Family" }
        });

        var result = await controller.Create(new RoomTypeCreateDto { TypeName = "Family", MaxOccupancy = 4, PricePerNight = 3000 });

        var created = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(nameof(RoomTypeController.GetById), created.ActionName);
    }

    [Fact]
    public async Task UpdatePrice_WhenRoomTypeExists_ReturnsOk()
    {
        var controller = new RoomTypeController(new FakeRoomTypeService { UpdatePriceResult = true });

        var result = await controller.UpdatePrice(1, new RoomTypePriceUpdateDto { PricePerNight = 2500 });

        var ok = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse<object>>(ok.Value);
        Assert.Equal("Price updated.", response.Message);
    }

    [Fact]
    public async Task GetById_WhenRoomTypeMissing_ReturnsNotFound()
    {
        var controller = new RoomTypeController(new FakeRoomTypeService());

        var result = await controller.GetById(404);

        var notFound = Assert.IsType<NotFoundObjectResult>(result);
        var response = Assert.IsType<ApiResponse<object>>(notFound.Value);
        Assert.False(response.Success);
    }

    [Fact]
    public async Task Update_WhenRoomTypeMissing_ReturnsNotFound()
    {
        var controller = new RoomTypeController(new FakeRoomTypeService { UpdateResult = false });

        var result = await controller.Update(404, new RoomTypeUpdateDto { TypeName = "Suite" });

        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task Delete_WhenRoomTypeMissing_ReturnsNotFound()
    {
        var controller = new RoomTypeController(new FakeRoomTypeService { DeleteResult = false });

        var result = await controller.Delete(404);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task UpdatePrice_WhenRoomTypeMissing_ReturnsNotFound()
    {
        var controller = new RoomTypeController(new FakeRoomTypeService { UpdatePriceResult = false });

        var result = await controller.UpdatePrice(404, new RoomTypePriceUpdateDto { PricePerNight = 2500 });

        Assert.IsType<NotFoundObjectResult>(result);
    }

    private sealed class FakeRoomTypeService : IRoomTypeService
    {
        public List<RoomTypeDto> RoomTypes { get; set; } = [];
        public RoomTypeDto? RoomType { get; set; }
        public RoomTypeDto? CreatedRoomType { get; set; }
        public bool UpdateResult { get; set; }
        public bool UpdatePriceResult { get; set; }
        public bool DeleteResult { get; set; }

        public Task<List<RoomTypeDto>> GetAllAsync() => Task.FromResult(RoomTypes);
        public Task<RoomTypeDto?> GetByIdAsync(int id) => Task.FromResult(RoomType);
        public Task<List<RoomDto>> GetRoomsByTypeIdAsync(int id) => Task.FromResult(new List<RoomDto>());
        public Task<List<RoomTypeDto>> GetByCapacityAsync(int capacity) => Task.FromResult(RoomTypes);
        public Task<List<RoomTypeDto>> GetByPriceRangeAsync(decimal min, decimal max) => Task.FromResult(RoomTypes);
        public Task<List<RoomTypeDto>> GetAvailableRoomTypesAsync() => Task.FromResult(RoomTypes);
        public Task<RoomTypeDto?> GetRoomTypeDetailsByIdAsync(int id) => Task.FromResult(RoomType);
        public Task<RoomTypeDto> CreateAsync(RoomTypeCreateDto dto) => Task.FromResult(CreatedRoomType ?? new RoomTypeDto { RoomTypeId = 1, TypeName = dto.TypeName });
        public Task<bool> UpdateAsync(int id, RoomTypeUpdateDto dto) => Task.FromResult(UpdateResult);
        public Task<bool> UpdatePriceAsync(int id, RoomTypePriceUpdateDto dto) => Task.FromResult(UpdatePriceResult);
        public Task<bool> DeleteAsync(int id) => Task.FromResult(DeleteResult);
    }
}
