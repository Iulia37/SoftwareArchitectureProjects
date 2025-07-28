using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.DTO.Models
{
    public class ProjectDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required!")]
        [MaxLength(100, ErrorMessage = "Name can't be longer than 100 characters!")]
        [RegularExpression(@"^[A-Z][a-zA-Z0-9\s]*$", ErrorMessage = "Name must start with a capital letter and contain only letters, numbers and spaces!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required!")]
        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required(ErrorMessage = "Deadline is required!")]
        public DateTime Deadline { get; set; }

        public bool IsCompleted { get; set; }

        public int UserId { get; set; }
    }
}
