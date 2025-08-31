using Microsoft.AspNetCore.Identity;
using UserService.API.Data;
using UserService.API.Models;

namespace UserService.API.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public User getUserById(int id, int currentUser)
        {
            var user = _userRepository.getUserById(id);
            if(user == null)
            {
                throw new KeyNotFoundException("User not found!");
            }
            if (user.Id != currentUser)
            {
                throw new UnauthorizedAccessException("You cannot access this user!");
            }
            return user;
        }
        public User getUserByUsername(string username)
        {
            return _userRepository.getUserByUsername(username);
        }
    }
}
