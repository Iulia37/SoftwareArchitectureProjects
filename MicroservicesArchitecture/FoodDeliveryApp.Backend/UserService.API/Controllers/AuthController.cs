using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UserService.API.Models;
using UserService.API.Services;
using UserService.API.DTOs;
using Nelibur.ObjectMapper;

namespace JwtAuthDotNet9.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public ActionResult<User> Register(RegisterUserDTO registerUser)
        {
            var user = _authService.registerUser(TinyMapper.Map<User>(registerUser));
            if (user is null)
                return BadRequest("Username already exists!");

            return Ok(new { message = "User registered succesfully!" });
        }

        [HttpPost("login")]
        public ActionResult<string> Login(LoginUserDTO loginUser)
        {
            var userToken = _authService.authenticateUser(TinyMapper.Map<User>(loginUser));
            if (userToken is null)
                return BadRequest("Invalid username or password!");

            return Ok(userToken);
        }
    }
}