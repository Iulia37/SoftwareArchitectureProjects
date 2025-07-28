using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Models;

namespace TaskManager.DataAccess.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<TaskItem> TaskItems { get; set; }
        public DbSet<Project> Projects { get; set; }

		public DbSet<User> Users { get; set; }
	}
}

