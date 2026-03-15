using back_3lb.Models;
using Microsoft.EntityFrameworkCore;
namespace back_3lb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}