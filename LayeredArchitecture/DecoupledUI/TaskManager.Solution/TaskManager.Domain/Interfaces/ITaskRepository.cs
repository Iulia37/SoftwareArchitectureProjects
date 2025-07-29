using TaskManager.Domain.Models;

namespace TaskManager.Domain.Repositories
{
    public interface ITaskRepository
    {
        TaskItem GetTaskById(int id);
        IEnumerable<TaskItem> GetTasksByProjectId(int projectId);
        void AddTask(TaskItem task);
        void UpdateTask(TaskItem newTask);
        void DeleteTask(int id);
    }
}