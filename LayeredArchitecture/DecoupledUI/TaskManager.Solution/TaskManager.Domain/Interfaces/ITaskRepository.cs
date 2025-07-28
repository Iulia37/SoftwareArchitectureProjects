using TaskManager.Domain.Models;

namespace TaskManager.Domain.Repositories
{
    public interface ITaskRepository
    {
        TaskItem GetById(int id);
        IEnumerable<TaskItem> GetProjectTasks(int projectId);
        void Add(TaskItem task);
        void Update(TaskItem newTask);
        void Delete(int id);
    }
}