using UserService.API.Models;

namespace UserService.API.Services
{
    public interface IUserService
    {
        public User getUserById(int id, int currentUser);
        public User getUserByUsername(string username);

    }
}
