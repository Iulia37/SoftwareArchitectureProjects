using Microsoft.AspNetCore.Mvc;
using UserService.API.Models;
using UserService.API.Services;

namespace UserService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public IActionResult getUserById(int id)
        {
            var user = _userService.getUserById(id);
            if (user == null)
            {
                return BadRequest("User not found!");
            }
            return Ok(user);
        }
    }
}
