using System.Net.Http.Json;
using Kritik.Shared.Models;

namespace Kritik.App.Services;

public class AuthService
{
    private readonly HttpClient _httpClient;
    private User? _currentUser;

    public AuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<User?> LoginAsync(string email, string password)
    {
        try
        {
            var request = new LoginRequest { Username = email, Password = password };
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", request);

            if (response.IsSuccessStatusCode)
            {
                var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();
                if (loginResponse != null)
                {
                    _currentUser = new User
                    {
                        Id = email, // Or use a real ID from response if available
                        Username = email,
                        FullName = loginResponse.FullName,
                        Role = loginResponse.Role
                    };
                    return _currentUser;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Login failed: {ex.Message}");
        }
        return null;
    }

    public async Task LogoutAsync()
    {
        _currentUser = null;
        await Task.CompletedTask;
    }
    
    public User? GetCurrentUser()
    {
        return _currentUser;
    }
}
