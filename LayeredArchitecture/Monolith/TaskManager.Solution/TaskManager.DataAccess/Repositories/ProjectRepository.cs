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
        public Project GetById(int id)
        {
            var project = _context.Projects.Find(id);
            return project;
        }
        public IEnumerable<Project> GetAll()
        {
            return _context.Projects.ToList();
        }
        public void Add(Project project)
        {
            _context.Projects.Add(project);
            _context.SaveChanges();
        }
        public void Update(int id, Project newProject)
        {
            var project = _context.Projects.Find(id);

            if(project != null)
            {
                project.Name = newProject.Name;
                project.Description = newProject.Description;
                project.Deadline = newProject.Deadline;
            }
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            var project = _context.Projects.Find(id);

            if (project != null)
            {
                _context.Projects.Remove(project);
                _context.SaveChanges();
            }
        }
    }
}
