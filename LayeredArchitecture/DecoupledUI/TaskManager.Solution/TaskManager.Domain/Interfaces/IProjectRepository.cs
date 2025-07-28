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
        IEnumerable<Project> GetAll(int userId);
        void Add(Project project);
        void Update(Project newProject);
        void Delete(int id);
    }
}
