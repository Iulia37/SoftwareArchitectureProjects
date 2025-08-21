using UserService.API.Models;

namespace UserService.API.Services
{
    public interface IUserService
    {
        public User getUserById(int id);
        public User getUserByUsername(string username);

    }
}
