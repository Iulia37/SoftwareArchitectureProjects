using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using TaskManager.DTO.Models;

public class UserService
{
    private readonly HttpClient _http;

    public UserService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<UserDTO>> GetAllUsersAsync()
    {
        return await _http.GetFromJsonAsync<List<UserDTO>>("api/users");
    }

    public async Task<UserDTO> GetUserByIdAsync(int id)
    {
        return await _http.GetFromJsonAsync<UserDTO>($"api/users/{id}");
    }

    public async Task RegisterAsync(RegisterUserDTO registerDto)
    {
        var response = await _http.PostAsJsonAsync("api/users/register", registerDto);
        if (!response.IsSuccessStatusCode)
        {
            var errorMsg = await response.Content.ReadAsStringAsync();
            throw new ApplicationException(errorMsg);
        }
    }

    public async Task<UserDTO> LoginAsync(LoginUserDTO loginDto)
    {
        var response = await _http.PostAsJsonAsync("api/users/login", loginDto);
        if (!response.IsSuccessStatusCode)
        {
            var errorMsg = await response.Content.ReadAsStringAsync();
            throw new ApplicationException(errorMsg);
        }
        return await response.Content.ReadFromJsonAsync<UserDTO>();
    }

    public async Task EditUserAsync(UserDTO user)
    {
        var response = await _http.PutAsJsonAsync($"api/users/{user.Id}", user);
        if (!response.IsSuccessStatusCode)
        {
            var errorMsg = await response.Content.ReadAsStringAsync();
            throw new ApplicationException(errorMsg);
        }
    }

    public async Task DeleteUserAsync(int id)
    {
        var response = await _http.DeleteAsync($"api/users/{id}");
        if (!response.IsSuccessStatusCode)
        {
            var errorMsg = await response.Content.ReadAsStringAsync();
            throw new ApplicationException(errorMsg);
        }
    }
}