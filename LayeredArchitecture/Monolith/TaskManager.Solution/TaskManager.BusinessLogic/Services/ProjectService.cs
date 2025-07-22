using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.Interfaces;
using TaskManager.Domain.Models;

namespace TaskManager.BusinessLogic.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepo;

        public ProjectService(IProjectRepository projectRepo)
        {
            _projectRepo = projectRepo;
        }

        public IEnumerable<Project> GetAllProjects()
        {
            return _projectRepo.GetAll();
        }

        public Project GetProjectById(int id)
        {
            return _projectRepo.GetById(id);
        }

        public void AddProject(Project project)
        {
            if (string.IsNullOrWhiteSpace(project.Name))
                throw new ArgumentException("Project name is required!");

            if (project.Name.Length > 100)
                throw new ArgumentException("Project name can't be longer than 100 characters!");

            var existing = _projectRepo.GetAll().FirstOrDefault(p => p.Name == project.Name);
            if (existing != null)
                throw new InvalidOperationException("A project with this name already exists!");

            project.CreatedDate = DateTime.Now;

            _projectRepo.Add(project);
        }


        public void UpdateProject(int id, Project newProject)
        {
            var project = _projectRepo.GetById(id);
            if (project == null)
                throw new ArgumentException("Project not found!");

            if (string.IsNullOrWhiteSpace(newProject.Name))
                throw new ArgumentException("Project name is required!");

            if (newProject.Name.Length > 100)
                throw new ArgumentException("Project name can't be longer than 100 characters!");

            var existing = _projectRepo.GetAll().FirstOrDefault(p => p.Name == newProject.Name && p.Id != id);
            if (existing != null)
                throw new InvalidOperationException("Another project with this name already exists!");

            project.Name = newProject.Name;
            project.Description = newProject.Description;
            project.Deadline = newProject.Deadline;

            _projectRepo.Update(id, project);
        }


        public void DeleteProject(int id)
        {
            var project = _projectRepo.GetById(id);
            if (project == null)
                throw new ArgumentException("Project not found!");

            _projectRepo.Delete(id);
        }
    }
}
