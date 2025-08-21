using UserService.API.Models;

namespace UserService.API.Services
{
    public interface IAuthService
    {
        public User registerUser(User newUser);

        public string authenticateUser(User registerUser);
    }
}
