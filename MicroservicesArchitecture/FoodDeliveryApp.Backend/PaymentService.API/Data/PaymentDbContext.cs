using Microsoft.EntityFrameworkCore;
using PaymentService.API.Models;

namespace PaymentService.API.Data
{
    public class PaymentDbContext: DbContext
    {
        public PaymentDbContext(DbContextOptions<PaymentDbContext> options) : base(options)
        {
        }
        public DbSet<Payment> Payments { get; set; }
    }
}
