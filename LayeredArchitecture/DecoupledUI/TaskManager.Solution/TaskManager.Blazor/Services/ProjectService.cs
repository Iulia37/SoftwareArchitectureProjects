using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using TaskManager.DTO.Models;

public class ProjectService
{
    private readonly HttpClient _http;

    public ProjectService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<ProjectDTO>> GetAllProjectsForUserAsync(int userId)
    {
        return await _http.GetFromJsonAsync<List<ProjectDTO>>($"api/projects/user/{userId}");
    }

    public async Task<ProjectDTO> GetProjectByIdAsync(int id)
    {
        return await _http.GetFromJsonAsync<ProjectDTO>($"api/projects/{id}");
    }

    public async Task<ProjectDTO> CreateProjectAsync(ProjectDTO project)
    {
        var response = await _http.PostAsJsonAsync("api/projects", project);
        if (!response.IsSuccessStatusCode)
        {
            var errorMsg = await response.Content.ReadAsStringAsync();
            throw new ApplicationException(errorMsg);
        }
        return await response.Content.ReadFromJsonAsync<ProjectDTO>();
    }

    public async Task EditProjectAsync(ProjectDTO project)
    {
        var response = await _http.PutAsJsonAsync($"api/projects/{project.Id}", project);
        if (!response.IsSuccessStatusCode)
        {
            var errorMsg = await response.Content.ReadAsStringAsync();
            throw new ApplicationException(errorMsg);
        }
    }

    public async Task DeleteProjectAsync(int id)
    {
        var response = await _http.DeleteAsync($"api/projects/{id}");
        response.EnsureSuccessStatusCode();
    }

    public async Task MarkProjectCompletedAsync(int id)
    {
        var response = await _http.PostAsync($"api/projects/{id}/complete", null);
        if (!response.IsSuccessStatusCode)
        {
            var errorMsg = await response.Content.ReadAsStringAsync();
            throw new ApplicationException(errorMsg);
        }
    }
}