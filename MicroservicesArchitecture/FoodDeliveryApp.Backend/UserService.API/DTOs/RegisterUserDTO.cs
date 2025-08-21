using System.ComponentModel.DataAnnotations;

namespace UserService.API.DTOs
{
    public class RegisterUserDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Username required!")]
        [MaxLength(30, ErrorMessage = "Username can't be longer than 30 characters!")]
        [MinLength(3, ErrorMessage = "Username must be at least 3 characters long!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password required!")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$", ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, and one number")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email required!")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Role required!")]
        public string Role { get; set; }
    }
}
