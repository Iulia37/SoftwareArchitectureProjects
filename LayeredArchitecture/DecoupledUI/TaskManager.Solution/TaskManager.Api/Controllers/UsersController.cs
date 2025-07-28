using Microsoft.AspNetCore.Mvc;
using Nelibur.ObjectMapper;
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
                return Ok(TinyMapper.Map<UserDTO>(authenticatedUser));
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
            return Ok(users.Select(u => TinyMapper.Map<UserDTO>(u)));
        }

        [HttpGet("{id}")]
        public ActionResult<UserDTO> GetById(int id)
        {
            var user = _userService.GetById(id);
            if (user == null)
                return NotFound();
            return Ok(TinyMapper.Map<UserDTO>(user));
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id, [FromBody] UserDTO userDto)
        {
            if (id != userDto.Id)
                return BadRequest("ID mismatch.");

            try
            {
                var user = TinyMapper.Map<User>(userDto);
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