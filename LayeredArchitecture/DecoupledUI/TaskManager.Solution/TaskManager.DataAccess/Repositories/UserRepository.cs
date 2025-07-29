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
	public class UserRepository : IUserRepository
	{
		private readonly ApplicationDbContext _context;
		private readonly IProjectRepository _projectRepo;
		public UserRepository(ApplicationDbContext context, IProjectRepository projectRepo)
		{
			_context = context;
			_projectRepo = projectRepo;
        }

		public User GetUserById(int id)
		{
			return _context.Users.Find(id);
		}
		public User GetUserByUsername(string username)
		{
			return _context.Users.FirstOrDefault(u => u.Username == username);
		}
		public void AddUser(User user)
		{
			_context.Users.Add(user);
		    _context.SaveChanges();
		}
		public void UpdateUser(User newUser)
		{
			var user = _context.Users.Find(newUser.Id);

			if (user != null)
			{
				user.Username = newUser.Username;
				user.Password = newUser.Password;
			}

			_context.SaveChanges();
		}
		public void DeleteUser(int id)
		{
			var user = _context.Users.Find(id);

			var projects = _projectRepo.GetProjectsByUserId(id);

			if (user != null)
			{
				_context.Remove(user);

				foreach(var p in projects)
				{
					_projectRepo.DeleteProject(p.Id);
				}

				_context.SaveChanges();
			}
		}
		public IEnumerable<User> GetAllUsers()
		{
			return _context.Users.ToList();
		}
	}
}
