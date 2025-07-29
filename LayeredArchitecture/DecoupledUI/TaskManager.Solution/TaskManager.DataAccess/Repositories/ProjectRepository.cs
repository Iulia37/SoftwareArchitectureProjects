using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.DataAccess.Contexts;
using TaskManager.Domain.Interfaces;
using TaskManager.Domain.Models;

namespace TaskManager.DataAccess.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext _context;

        public ProjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Project GetProjectById(int id)
        {
            var project = _context.Projects.Find(id);
            return project;
        }
        public IEnumerable<Project> GetProjectsByUserId(int userId)
        {
            return _context.Projects.Where(p => p.UserId == userId);
        }
        public void AddProject(Project project)
        {
            _context.Projects.Add(project);
            _context.SaveChanges();
        }
        public void UpdateProject(Project newProject)
        {
            var project = _context.Projects.Find(newProject.Id);

            if(project != null)
            {
                project.Name = newProject.Name;
                project.Description = newProject.Description;
                project.Deadline = newProject.Deadline;
            }
            _context.SaveChanges();
        }
        public void DeleteProject(int id)
        {
            var project = _context.Projects.Find(id);
            var tasks = _context.TaskItems.Where(t => t.ProjectId == id);

            if (project != null)
            {
                _context.Projects.Remove(project);

                foreach(var t in tasks)
                {
                    _context.TaskItems.Remove(t);
                }

                _context.SaveChanges();
            }
        }
    }
}
