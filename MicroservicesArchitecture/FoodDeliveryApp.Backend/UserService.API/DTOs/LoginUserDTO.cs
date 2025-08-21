using System.ComponentModel.DataAnnotations;

namespace UserService.API.DTOs
{
    public class LoginUserDTO
    {
        [Required(ErrorMessage = "Username required!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password required!")]
        public string Password { get; set; }
    }
}
