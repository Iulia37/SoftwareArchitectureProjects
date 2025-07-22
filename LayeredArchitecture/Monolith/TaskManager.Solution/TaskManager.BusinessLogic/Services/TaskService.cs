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

        public IEnumerable<TaskItem> GetAllProjectTasks(int projectId)
        {
            return _taskRepo.GetProjectTasks(projectId);
        }

        public TaskItem GetTaskById(int id)
        {
            return _taskRepo.GetById(id);
        }

        public void AddTask(TaskItem task)
        {
            if (string.IsNullOrWhiteSpace(task.Title))
                throw new ArgumentException("Task title is required!");

            if (task.Deadline < DateTime.Now)
                throw new ArgumentException("Deadline can not be in the past!");

            task.CreatedDate = DateTime.Now;
            task.IsCompleted = false;

            _taskRepo.Add(task);
        }

        public void UpdateTask(int id, TaskItem updatedTask)
        {
            var task = _taskRepo.GetById(id);
            if (task == null)
                throw new ArgumentException("There is no task with that id");

            if (string.IsNullOrWhiteSpace(updatedTask.Title))
                throw new ArgumentException("Task title is required!");

            if (updatedTask.Deadline < DateTime.Now)
                throw new ArgumentException("Deadline can not be in the past!");

            task.Title = updatedTask.Title;
            task.Description = updatedTask.Description;
            task.Deadline = updatedTask.Deadline;
            _taskRepo.Update(id, task);
        }

        public void DeleteTask(int id)
        {
            var task = _taskRepo.GetById(id);
            if (task == null)
                throw new ArgumentException("There is no task with that id");

            _taskRepo.Delete(id);
        }

        public void CompleteTask(int id)
        {
            var task = _taskRepo.GetById(id);
            if (task == null) return;

            task.IsCompleted = true;
            _taskRepo.Update(id, task);
        }
    }
}