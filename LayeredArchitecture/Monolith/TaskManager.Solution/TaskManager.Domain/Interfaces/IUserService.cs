using TaskManager.Domain.Models;

namespace TaskManager.Domain.Interfaces
{
	public interface IUserService
	{
		User GetById(int id);
		User GetByUsername(string username);
		void Add(User user);
		void Update(User user);
		void Delete(int id);
		IEnumerable<User> GetAll();
		User Authenticate(string username, string password);
		void Register(string username, string password);
		bool UsernameExists(string username);
	}
}
