using Microsoft.EntityFrameworkCore;
using System.Threading.RateLimiting;

namespace WheelFactory.Models
{
    public class WheelContext:DbContext
    {
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Orders> Order { get; set; }
        public DbSet<Colors> Color { get; set; }
        public DbSet<SandBlastingLevels> SandBlasting { get; set; }
        public DbSet<PaintType> Paint { get; set; }
        public DbSet<Rating> Rate { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=.\\SQLEXPRESS;initial catalog=wheelfactory;User ID=sa;Password=Pass@123;TrustServerCertificate=true");
            base.OnConfiguring(optionsBuilder);
        }

    }
}
