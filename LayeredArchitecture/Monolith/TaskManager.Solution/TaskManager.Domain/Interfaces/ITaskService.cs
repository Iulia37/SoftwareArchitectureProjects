using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.Models;

namespace TaskManager.Domain.Interfaces
{
    public interface ITaskService
    {
        IEnumerable<TaskItem> GetAllProjectTasks(int projectId);

        TaskItem GetTaskById(int id);

        void AddTask(TaskItem task);

        void UpdateTask(int id, TaskItem newTask);

        void DeleteTask(int id);

        void CompleteTask(int id);
    }
}
