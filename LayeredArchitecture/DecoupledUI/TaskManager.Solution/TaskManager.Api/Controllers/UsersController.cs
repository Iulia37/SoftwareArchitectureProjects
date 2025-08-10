using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Nelibur.ObjectMapper;
using System.Text;
using TaskManager.Domain.Interfaces;
using TaskManager.Domain.Models;
using TaskManager.DTO.Models;
using Microsoft.AspNetCore.Authorization;

namespace TaskManager.API.Controllers
{
    [Authorize]
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
       
        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<UserDTO>> GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            return Ok(users.Select(u => TinyMapper.Map<UserDTO>(u)));
        }

        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<UserDTO> GetUserById(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
                return BadRequest();
            return Ok(TinyMapper.Map<UserDTO>(user));
        }

        [Authorize]
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

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
                return BadRequest();

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