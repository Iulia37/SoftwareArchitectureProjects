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
        public User getUserById(int id)
        {
            return _userRepository.getUserById(id);
        }
        public User getUserByUsername(string username)
        {
            return _userRepository.getUserByUsername(username);
        }
    }
}
