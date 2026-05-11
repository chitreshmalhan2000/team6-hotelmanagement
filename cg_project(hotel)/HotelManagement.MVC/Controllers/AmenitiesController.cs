using HotelManagement.MVC.Services;
using HotelManagement.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.MVC.Controllers;

public class AmenitiesController(ApiClientService apiClient) : Controller
{
    public async Task<IActionResult> Index()
    {
        var response = await apiClient.GetAsync<ApiResponse<List<AmenityViewModel>>>("api/amenities");
        return View(response?.Data ?? []);
    }

    [HttpGet]
    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(CreateAmenityViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        var response = await apiClient.PostAsync<CreateAmenityViewModel, ApiResponse<AmenityViewModel>>("api/amenities", model);
        if (response?.Success == true) return RedirectToAction(nameof(Index));
        ModelState.AddModelError(string.Empty, "Failed to create amenity. Make sure you are logged in as Admin.");
        return View(model);
    }
}
