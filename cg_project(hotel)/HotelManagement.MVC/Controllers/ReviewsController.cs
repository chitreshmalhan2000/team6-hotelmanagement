using HotelManagement.MVC.Services;
using HotelManagement.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.MVC.Controllers;

public class ReviewsController(ApiClientService apiClient) : Controller
{
    public async Task<IActionResult> Index()
    {
        var response = await apiClient.GetAsync<ApiResponse<List<ReviewViewModel>>>("api/reviews");
        return View(response?.Data ?? []);
    }

    [HttpGet]
    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(CreateReviewViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        var response = await apiClient.PostAsync<CreateReviewViewModel, ApiResponse<ReviewViewModel>>("api/reviews", model);
        if (response?.Success == true) return RedirectToAction(nameof(Index));
        ModelState.AddModelError(string.Empty, "Failed to create review.");
        return View(model);
    }
}
