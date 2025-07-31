using TaskManager.Domain.Models;

namespace TaskManager.Domain.Interfaces
{
	public interface IUserService
	{
		User GetUserById(int id);
		User GetUserByUsername(string username);
		void AddUser(User user);
		void UpdateUser(User user);
		void DeleteUser(int id);
		IEnumerable<User> GetAllUsers();
		User AuthenticateUser(string username, string password);
		void RegisterUser(string username, string password, string email);
		bool UsernameExists(string username);
	}
}
