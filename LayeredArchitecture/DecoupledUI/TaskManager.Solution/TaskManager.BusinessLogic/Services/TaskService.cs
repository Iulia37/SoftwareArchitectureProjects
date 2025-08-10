using System.Collections.Generic;
using System.Security.Claims;
using TaskManager.Domain.Interfaces;
using TaskManager.Domain.Models;
using TaskManager.Domain.Repositories;

namespace TaskManager.BusinessLogic.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepo;
        private readonly IProjectRepository _projectRepo;

        public TaskService(ITaskRepository taskRepo, IProjectRepository projectRepo)
        {
            _taskRepo = taskRepo;
            _projectRepo = projectRepo;
        }

        public IEnumerable<TaskItem> GetTasksByProjectId(int projectId)
        {
            return _taskRepo.GetTasksByProjectId(projectId);
        }

        public TaskItem GetTaskById(int id, int currentUserId)
        {
            var task = _taskRepo.GetTaskById(id);

            if (task == null)
                throw new ArgumentException("Task not found!");

            var project = _projectRepo.GetProjectById(task.ProjectId);

            if(project.UserId != currentUserId)
            {
                throw new UnauthorizedAccessException("You cannot access this task!");
            }

            return task;
        }

        public void AddTask(TaskItem task)
        {
            if (task.Deadline < DateTime.Now)
                throw new ArgumentException("Deadline can not be in the past!");

            var project = _projectRepo.GetProjectById(task.ProjectId);
            if (project == null)
                throw new ArgumentException("Invalid project!");

            if (task.Deadline > project.Deadline)
                throw new ArgumentException("Task deadline cannot be after project's deadline!");


            task.CreatedDate = DateTime.Now;
            task.IsCompleted = false;

            _taskRepo.AddTask(task);
        }

        public void UpdateTask(TaskItem updatedTask)
        {
            var task = _taskRepo.GetTaskById(updatedTask.Id);

            if (task == null)
                throw new ArgumentException("There is no task with that id");

            if (updatedTask.Deadline < DateTime.Now)
                throw new ArgumentException("Deadline can not be in the past!");

            var project = _projectRepo.GetProjectById(task.ProjectId);
            if (project == null)
                throw new ArgumentException("Invalid project!");

            if (task.Deadline > project.Deadline)
                throw new ArgumentException("Task deadline cannot be after project's deadline!");

            task.Title = updatedTask.Title;
            task.Description = updatedTask.Description;
            task.Deadline = updatedTask.Deadline;
            _taskRepo.UpdateTask(task);
        }

        public void DeleteTask(int id)
        {
            var task = _taskRepo.GetTaskById(id);
            if (task == null)
                throw new ArgumentException("There is no task with that id");

            _taskRepo.DeleteTask(id);
        }

        public void MarkTaskCompleted(int id)
        {
            var task = _taskRepo.GetTaskById(id);
            if (task == null) return;

            task.IsCompleted = true;
            _taskRepo.UpdateTask(task);
        }
    }
}