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
        Project GetById(int id);
        IEnumerable<Project> GetAll();
        void Add(Project project);
        void Update(int id, Project newProject);
        void Delete(int id);
    }
}
