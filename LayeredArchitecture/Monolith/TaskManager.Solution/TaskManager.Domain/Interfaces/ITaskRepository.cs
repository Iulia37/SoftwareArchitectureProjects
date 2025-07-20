using TaskManager.Domain.Models;

namespace TaskManager.Domain.Repositories
{
    public interface ITaskRepository
    {
        TaskItem GetById(int id);
        IEnumerable<TaskItem> GetAll();
        void Add(TaskItem task);
        void Update(int id, TaskItem newTask);
        void Delete(int id);
    }
}