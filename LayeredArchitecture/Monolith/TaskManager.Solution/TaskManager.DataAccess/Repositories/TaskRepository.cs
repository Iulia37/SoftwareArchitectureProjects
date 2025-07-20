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

        public TaskItem GetById(int id)
        {
            return _context.TaskItems.Find(id);
        }

        public IEnumerable<TaskItem> GetAll()
        {
            return _context.TaskItems.ToList();
        }

        public void Add(TaskItem task)
        {
            _context.TaskItems.Add(task);
            _context.SaveChanges();
        }

        public void Update(int id, TaskItem newTask)
        {
            var task = _context.TaskItems.Find(id);

            task.Title = newTask.Title;
            task.Description = newTask.Description;

            _context.SaveChanges();
        }

        public void Delete(int id)
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