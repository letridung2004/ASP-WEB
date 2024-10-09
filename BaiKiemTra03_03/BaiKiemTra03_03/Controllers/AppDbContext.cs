using BaiKiemTra03_03.Models;
using Microsoft.EntityFrameworkCore;

namespace BaiKiemTra03_03.Controllers
{
    public class AppDbContext : DbContext
    {
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("your_connection_string");
        }
    }

}