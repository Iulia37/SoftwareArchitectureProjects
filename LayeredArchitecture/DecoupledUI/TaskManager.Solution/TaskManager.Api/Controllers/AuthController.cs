using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Domain.Models;
using Nelibur.ObjectMapper;
using TaskManager.Domain.Interfaces;
using TaskManager.DTO.Models;

namespace JwtAuthDotNet9.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController: ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("register")]
        public ActionResult<User> Register(RegisterUserDTO registerUser)
        {
            var user = authService.Register(TinyMapper.Map<User>(registerUser));
            if (user is null)
                return BadRequest("Username already exists!");

            return Ok(new {message = "User registered succesfully!"});
        }

        [HttpPost("login")]
        public ActionResult<string> Login(LoginUserDTO loginUser)
        {
            var userToken = authService.Login(TinyMapper.Map<User>(loginUser));
            if (userToken is null)
                return BadRequest("Invalid username or password!");

            return Ok(userToken);
        }
    }
}
