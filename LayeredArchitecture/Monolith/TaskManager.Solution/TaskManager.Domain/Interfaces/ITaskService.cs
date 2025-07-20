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
        public IEnumerable<TaskItem> GetAllTasks();

        public TaskItem GetTaskById(int id);

        public void AddTask(TaskItem task);

        public void UpdateTask(int id, TaskItem newTask);

        public void DeleteTask(int id);

        public void CompleteTask(int id);
    }
}
