using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using TaskManager.DTO.Models;

public class TaskService
{
    private readonly HttpClient _http;

    public TaskService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<TaskItemDTO>> GetTasksForProjectAsync(int projectId)
    {
        return await _http.GetFromJsonAsync<List<TaskItemDTO>>($"api/tasks/project/{projectId}");
    }

    public async Task<TaskItemDTO> GetTaskByIdAsync(int id)
    {
        return await _http.GetFromJsonAsync<TaskItemDTO>($"api/tasks/{id}");
    }

    public async Task<TaskItemDTO> CreateTaskAsync(TaskItemDTO task)
    {
        var response = await _http.PostAsJsonAsync("api/tasks", task);
        if (!response.IsSuccessStatusCode)
        {
            var errorMsg = await response.Content.ReadAsStringAsync();
            throw new ApplicationException(errorMsg);
        }
        return await response.Content.ReadFromJsonAsync<TaskItemDTO>();
    }

    public async Task EditTaskAsync(TaskItemDTO task)
    {
        var response = await _http.PutAsJsonAsync($"api/tasks/{task.Id}", task);
        if (!response.IsSuccessStatusCode)
        {
            var errorMsg = await response.Content.ReadAsStringAsync();
            throw new ApplicationException(errorMsg);
        }
    }

    public async Task DeleteTaskAsync(int id)
    {
        var response = await _http.DeleteAsync($"api/tasks/{id}");
        response.EnsureSuccessStatusCode();
    }

    public async Task MarkTaskCompletedAsync(int id)
    {
        var response = await _http.PostAsync($"api/tasks/{id}/complete", null);
        response.EnsureSuccessStatusCode();
    }
}