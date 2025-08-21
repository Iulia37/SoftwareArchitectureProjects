using Microsoft.EntityFrameworkCore;
using UserService.API.Models;

namespace UserService.API.Data
{
    public class UserDbContext: DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) 
            : base(options) 
        {
        }
        public DbSet<User> Users { get; set; }
    }
}
