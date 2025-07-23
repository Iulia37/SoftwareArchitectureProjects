using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.Models;

namespace TaskManager.Domain.Interfaces
{
    public interface IProjectService
    {
        IEnumerable<Project> GetAllProjects(int userId);

        Project GetProjectById(int id);

        void AddProject(Project project);

        void UpdateProject(Project newProject);

        void DeleteProject(int id);

        public void MarkCompleted(int id);
    }
}
