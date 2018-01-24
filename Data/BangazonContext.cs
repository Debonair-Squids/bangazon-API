using Microsoft.EntityFrameworkCore;
using BangazonAPI.Models;

namespace BangazonAPI.Data
{
    public class BangazonContext : DbContext
    {
        public BangazonContext(DbContextOptions<BangazonContext> options)
            : base(options)
        { }

        public DbSet<Employee> Employee { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Training> Training { get; set; }
        public DbSet<EmployeeTraining> EmployeeTraining { get; set; }


// USE THIS LATER FOR ORDER CREATIONS
        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     modelBuilder.Entity<Order>()
        //         .Property(b => b.DateCreated)
        //         .HasDefaultValueSql("strftime('%Y-%m-%d %H:%M:%S')");
        // }
    }
}