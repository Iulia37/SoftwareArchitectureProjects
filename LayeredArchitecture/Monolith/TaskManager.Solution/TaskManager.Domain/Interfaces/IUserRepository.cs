using TaskManager.Domain.Models;

namespace TaskManager.Domain.Interfaces
{
	public interface IUserRepository
	{
		User GetById(int id);
		User GetByUsername(string username);
		void Add(User user);
		void Update(User user);
		void Delete(int id);
		IEnumerable<User> GetAll();
	}
}
