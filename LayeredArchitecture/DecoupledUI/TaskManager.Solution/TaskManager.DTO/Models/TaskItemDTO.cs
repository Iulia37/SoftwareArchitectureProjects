using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.DTO.Models
{
    public class TaskItemDTO
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }

        [Required(ErrorMessage = "Title is required!")]

        [MaxLength(100, ErrorMessage = "Title can't be longer than 100 characters!")]
        [RegularExpression(@"^[A-Z].*", ErrorMessage = "Title must start with a capital letter!")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required!")]
        [MaxLength(1000, ErrorMessage = "Description can't be longer than 1000 characters!")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Deadline is required!")]
        public DateTime Deadline { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
