using TaskManager.Domain.Models;
using TaskManager.Domain.Repositories;
using TaskManager.Domain.Interfaces;
using System.Collections.Generic;

namespace TaskManager.Business.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepo;

        public TaskService(ITaskRepository taskRepo)
        {
            _taskRepo = taskRepo;
        }

        public IEnumerable<TaskItem> GetAllTasks()
        {
            return _taskRepo.GetAll();
        }

        public TaskItem GetTaskById(int id)
        {
            return _taskRepo.GetById(id);
        }

        public void AddTask(TaskItem task)
        {
            _taskRepo.Add(task);
        }

        public void UpdateTask(int id, TaskItem task)
        {
            _taskRepo.Update(id, task);
        }

        public void DeleteTask(int id)
        {
            _taskRepo.Delete(id);
        }

        public void CompleteTask(int id)
        {
            /*var task = _taskRepo.GetById(id);
            if (task == null) return;

            task.IsCompleted = true;
            _taskRepo.Update(task);*/
        }
    }
}