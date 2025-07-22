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
        IEnumerable<Project> GetAllProjects();

        Project GetProjectById(int id);

        void AddProject(Project project);

        void UpdateProject(int id, Project newProject);

        void DeleteProject(int id);
    }
}
