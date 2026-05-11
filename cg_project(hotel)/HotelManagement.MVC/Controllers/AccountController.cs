using HotelManagement.MVC.Services;
using HotelManagement.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.MVC.Controllers;

public class AccountController(ApiClientService apiClient) : Controller
{
    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var response = await apiClient.PostAsync<object, ApiResponse<AuthResponseViewModel>>("api/auth/login", new
        {
            username = model.Username,
            password = model.Password
        });

        if (response?.Success == true && response.Data is not null)
        {
            HttpContext.Session.SetString("jwt_token", response.Data.Token);
            HttpContext.Session.SetString("username", response.Data.Username);
            HttpContext.Session.SetString("role", response.Data.Role);
            return RedirectToAction("Index", "Home");
        }

        ModelState.AddModelError(string.Empty, "Invalid username or password.");
        return View(model);
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction(nameof(Login));
    }
}
