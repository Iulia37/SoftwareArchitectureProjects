using Microsoft.AspNetCore.Mvc;
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

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        private UserDTO ToDto(User user)
        {
            if (user == null) return null;
            return new UserDTO
            {
                Id = user.Id,
                Username = user.Username
            };
        }

        private User FromDto(UserDTO dto)
        {
            if (dto == null) return null;
            return new User
            {
                Id = dto.Id,
                Username = dto.Username
            };
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserDTO userDto)
        {
            try
            {
                _userService.Register(userDto.Username, userDto.Password);
                return Ok("User registered successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public ActionResult<UserDTO> Login([FromBody] UserDTO userDto)
        {
            try
            {
                var authenticatedUser = _userService.Authenticate(userDto.Username, userDto.Password);
                return Ok(ToDto(authenticatedUser));
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserDTO>> GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users.Select(ToDto));
        }

        [HttpGet("{id}")]
        public ActionResult<UserDTO> GetById(int id)
        {
            var user = _userService.GetById(id);
            if (user == null)
                return NotFound();
            return Ok(ToDto(user));
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id, [FromBody] UserDTO userDto)
        {
            if (id != userDto.Id)
                return BadRequest("ID mismatch.");

            try
            {
                var user = FromDto(userDto);
                _userService.Update(user);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = _userService.GetById(id);
            if (user == null)
                return NotFound();

            try
            {
                _userService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}