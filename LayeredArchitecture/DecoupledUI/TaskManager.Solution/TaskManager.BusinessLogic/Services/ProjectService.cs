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

        public IEnumerable<Project> GetAllProjects(int userId)
        {
            return _projectRepo.GetAll(userId);
        }

        public Project GetProjectById(int id)
        {
            return _projectRepo.GetById(id);
        }

        public void AddProject(Project project)
        {
            if (string.IsNullOrWhiteSpace(project.Name))
                throw new ArgumentException("Project name is required!");

            if (string.IsNullOrWhiteSpace(project.Description))
                throw new ArgumentException("Project descriprion is required!");

            if (project.Name.Length > 100)
                throw new ArgumentException("Project name can't be longer than 100 characters!");

            var existing = _projectRepo.GetAll(project.UserId).FirstOrDefault(p => p.Name == project.Name);
            if (existing != null)
                throw new InvalidOperationException("A project with this name already exists!");

            if (project.Deadline < DateTime.Now)
                throw new ArgumentException("Deadline can not be in the past!");

            project.CreatedDate = DateTime.Now;

            _projectRepo.Add(project);
        }


        public void UpdateProject(Project newProject)
        {
            var project = _projectRepo.GetById(newProject.Id);
            if (project == null)
                throw new ArgumentException("Project not found!");

            if (string.IsNullOrWhiteSpace(newProject.Description))
                throw new ArgumentException("Project description is required!");

            if (string.IsNullOrWhiteSpace(newProject.Name))
                throw new ArgumentException("Project name is required!");

            if (newProject.Name.Length > 100)
                throw new ArgumentException("Project name can't be longer than 100 characters!");

            if (newProject.Deadline < DateTime.Now)
                throw new ArgumentException("Deadline can not be in the past!");

            project.Name = newProject.Name;
            project.Description = newProject.Description;
            project.Deadline = newProject.Deadline;

            _projectRepo.Update(project);
        }


        public void DeleteProject(int id)
        {
            var project = _projectRepo.GetById(id);
            if (project == null)
                throw new ArgumentException("Project not found!");

            _projectRepo.Delete(id);
        }

        public void MarkCompleted(int projectId)
        {
            var project = _projectRepo.GetById(projectId);
            if (project == null)
                throw new ArgumentException("Project not found!");

            project.IsCompleted = true;
            _projectRepo.Update(project);
        }
    }
}
