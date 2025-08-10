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
        IEnumerable<TaskItem> GetTasksByProjectId(int projectId);

        TaskItem GetTaskById(int id, int currentUserId);

        void AddTask(TaskItem task);

        void UpdateTask(TaskItem newTask);

        void DeleteTask(int id);

        void MarkTaskCompleted(int id);
    }
}
