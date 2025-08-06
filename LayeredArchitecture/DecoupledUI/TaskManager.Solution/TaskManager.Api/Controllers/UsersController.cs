using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Nelibur.ObjectMapper;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManager.Domain.Interfaces;
using TaskManager.Domain.Models;
using TaskManager.DTO.Models;

namespace TaskManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public UsersController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterUserDTO registerDto)
        {
            try
            {
                _userService.RegisterUser(registerDto.Username, registerDto.Password, registerDto.Email);
                return Ok("User registered successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginUserDTO loginDto)
        {
            try
            {
                var authenticatedUser = _userService.AuthenticateUser(loginDto.Username, loginDto.Password);

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes("super_secret_key_12345");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] {
                        new Claim(ClaimTypes.NameIdentifier, authenticatedUser.Id.ToString()),
                        new Claim(ClaimTypes.Name, authenticatedUser.Username),
                        new Claim(ClaimTypes.Email, authenticatedUser.Email)
                    }),

                    Expires = DateTime.UtcNow.AddHours(Convert.ToInt32(_configuration["Jwt:ExpireHours"])),
                    Issuer = _configuration["Jwt:Issuer"],
                    Audience = _configuration["Jwt:Audience"],
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature
                    )
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwt = tokenHandler.WriteToken(token);

                return Ok(new
                {
                    token,
                    user = TinyMapper.Map<UserDTO>(authenticatedUser)
                });
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }


        [HttpGet]
        public ActionResult<IEnumerable<UserDTO>> GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            return Ok(users.Select(u => TinyMapper.Map<UserDTO>(u)));
        }

        [HttpGet("{id}")]
        public ActionResult<UserDTO> GetUserById(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
                return NotFound();
            return Ok(TinyMapper.Map<UserDTO>(user));
        }

        [HttpPut("{id}")]
        public IActionResult EditUser(int id, [FromBody] UserDTO userDto)
        {
            if (id != userDto.Id)
                return BadRequest("ID mismatch!");

            try
            {
                var user = TinyMapper.Map<User>(userDto);
                _userService.UpdateUser(user);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
                return NotFound();

            try
            {
                _userService.DeleteUser(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}