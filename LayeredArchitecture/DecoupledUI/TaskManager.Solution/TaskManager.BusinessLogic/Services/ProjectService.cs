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

        public IEnumerable<Project> GetProjectsByUserId(int userId)
        {
            return _projectRepo.GetProjectsByUserId(userId);
        }

        public Project GetProjectById(int id, int currentUserId)
        {
            Project project = _projectRepo.GetProjectById(id);

            if (project == null)
                throw new ArgumentException("Project not found!");

            if (project.UserId != currentUserId)
            {
                throw new UnauthorizedAccessException("You cannot access this project!");
            }

            return project;
        }

        public void AddProject(Project project)
        {

            var existing = _projectRepo.GetProjectsByUserId(project.UserId).FirstOrDefault(p => p.Name == project.Name);
            if (existing != null)
                throw new InvalidOperationException("A project with this name already exists!");

            if (project.Deadline < DateTime.Now)
                throw new ArgumentException("Deadline can not be in the past!");

            project.CreatedDate = DateTime.Now;

            _projectRepo.AddProject(project);
        }


        public void UpdateProject(Project newProject)
        {
            var project = _projectRepo.GetProjectById(newProject.Id);
            if (project == null)
                throw new ArgumentException("Project not found!");

            if (newProject.Deadline < DateTime.Now)
                throw new ArgumentException("Deadline can not be in the past!");

            project.Name = newProject.Name;
            project.Description = newProject.Description;
            project.Deadline = newProject.Deadline;

            _projectRepo.UpdateProject(project);
        }


        public void DeleteProject(int id)
        {
            var project = _projectRepo.GetProjectById(id);
            if (project == null)
                throw new ArgumentException("Project not found!");

            _projectRepo.DeleteProject(id);
        }

        public void DeleteProjectsByUserId(int userId)
        {
            var projects = _projectRepo.GetProjectsByUserId(userId);

            foreach (var project in projects)
            {
                _projectRepo.DeleteProject(project.Id);
            }
        }

        public void MarkProjectCompleted(int projectId)
        {
            var project = _projectRepo.GetProjectById(projectId);
            if (project == null)
                throw new ArgumentException("Project not found!");

            project.IsCompleted = true;
            _projectRepo.UpdateProject(project);
        }
    }
}
