using System.Net.Http.Headers;
using System.Net.Http.Json;
using HotelManagement.MVC.ViewModels;

namespace HotelManagement.MVC.Services;

public class ApiClientService(IHttpClientFactory factory, IHttpContextAccessor accessor)
{
    public async Task<T?> GetAsync<T>(string endpoint)
    {
        var client = CreateClientWithAuth();
        return await client.GetFromJsonAsync<T>(endpoint);
    }

    public async Task<TResponse?> PostAsync<TRequest, TResponse>(string endpoint, TRequest payload)
    {
        var client = CreateClientWithAuth();
        var response = await client.PostAsJsonAsync(endpoint, payload);
        if (!response.IsSuccessStatusCode) return default;
        return await response.Content.ReadFromJsonAsync<TResponse>();
    }

    private HttpClient CreateClientWithAuth()
    {
        var client = factory.CreateClient("HotelApi");
        var token = accessor.HttpContext?.Session.GetString("jwt_token");
        if (!string.IsNullOrWhiteSpace(token))
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        return client;
    }
}
