using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.Models;

namespace TaskManager.Domain.Interfaces
{
    public interface IProjectRepository
    {
        Project GetProjectById(int id);
        IEnumerable<Project> GetProjectsByUserId(int userId);
        void AddProject(Project project);
        void UpdateProject(Project newProject);
        void DeleteProject(int id);
    }
}
