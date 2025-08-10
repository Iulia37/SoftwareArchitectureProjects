using TaskManager.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TaskManager.DataAccess.Repositories;
using TaskManager.Domain.Models;

namespace TaskManager.BusinessLogic.Services
{
    public class AuthService: IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration configuration;

        public AuthService(IUserRepository userRepo, IConfiguration configuration) {
            _userRepository = userRepo;
            this.configuration = configuration;
        }


        public string? Login(User loginUser)
        {
            var user = _userRepository.GetUserByUsername(loginUser.Username);
            if (user is null)
            {
                return null;
            }
            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.Password, loginUser.Password)
                == Microsoft.AspNetCore.Identity.PasswordVerificationResult.Failed)
            {
                return null;
            }

            return CreateToken(user);
        }

        public User? Register(User registerUser)
        {
            if (_userRepository.GetUserByUsername(registerUser.Username) != null)
            {
                return null;
            }

            var newUser = new User();
            var hashedPassword = new PasswordHasher<User>()
                .HashPassword(newUser, registerUser.Password);

            newUser.Username = registerUser.Username;
            newUser.Password = hashedPassword;
            newUser.Email = registerUser.Email;

            _userRepository.AddUser(newUser);

            return newUser;
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: configuration.GetSection("AppSettings:Issuer").Value,
                audience: configuration.GetSection("AppSettings:Audience").Value,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}