using UserService.API.Models;

namespace UserService.API.Data
{
    public interface IUserRepository
    {
        public User getUserById(int id);
        public User getUserByUsername(string username);
        public void addUser(User user);
        public void updateUser(User user);
        public void deleteUser(int id);
    }
}
