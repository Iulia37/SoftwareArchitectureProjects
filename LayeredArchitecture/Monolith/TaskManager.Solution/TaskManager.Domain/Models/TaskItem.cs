using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Domain.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Deadline { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
