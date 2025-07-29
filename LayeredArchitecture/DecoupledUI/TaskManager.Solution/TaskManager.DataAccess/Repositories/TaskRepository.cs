using TaskManager.Domain.Models;
using TaskManager.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using TaskManager.DataAccess.Contexts;

namespace TaskManager.DataAccess.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;

        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public TaskItem GetTaskById(int id)
        {
            return _context.TaskItems.Find(id);
        }

        public IEnumerable<TaskItem> GetTasksByProjectId(int projectId)
        {
            return _context.TaskItems.Where(t => t.ProjectId == projectId);
        }

        public void AddTask(TaskItem task)
        {
            _context.TaskItems.Add(task);
            _context.SaveChanges();
        }

        public void UpdateTask(TaskItem newTask)
        {
            var task = _context.TaskItems.Find(newTask.Id);

            if(task != null)
            {
                task.Title = newTask.Title;
                task.Description = newTask.Description;
            }

            _context.SaveChanges();
        }

        public void DeleteTask(int id)
        {
            var task = _context.TaskItems.Find(id);
            if (task != null)
            {
                _context.TaskItems.Remove(task);
                _context.SaveChanges();
            }
        }
    }
}