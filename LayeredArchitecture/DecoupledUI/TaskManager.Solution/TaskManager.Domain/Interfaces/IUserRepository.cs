using TaskManager.Domain.Models;

namespace TaskManager.Domain.Interfaces
{
	public interface IUserRepository
	{
		User GetUserById(int id);
		User GetUserByUsername(string username);
		void AddUser(User user);
		void UpdateUser(User user);
		void DeleteUser(int id);
		IEnumerable<User> GetAllUsers();
	}
}
