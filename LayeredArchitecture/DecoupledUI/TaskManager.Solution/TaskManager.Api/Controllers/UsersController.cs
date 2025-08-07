using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Nelibur.ObjectMapper;
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
                return Ok(new { message = "User registered successfully." });
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
                return Ok(TinyMapper.Map<UserDTO>(authenticatedUser));
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