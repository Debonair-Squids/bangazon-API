using Microsoft.EntityFrameworkCore;
using bangazon_inc.Models;


namespace bangazon_inc.Data
{
    public class BangazonContext : DbContext
    {
        public BangazonContext(DbContextOptions<BangazonContext> options)
            : base(options)
        { }

        public DbSet<Employee> Employee { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Training> Training { get; set; }
        public DbSet<Computer> Computer {get; set; }
        public DbSet<EmployeeComputer> EmployeeComputer {get; set; }
        public DbSet<OrderProduct> OrderProduct {get; set; }

        public DbSet<ProductType> ProductType { get; set; }
        public DbSet<Product> Product { get; set;}
        public DbSet<EmployeeTraining> EmployeeTraining { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<CustomerPayment> CustomerPayment { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<PaymentType> PaymentType { get; set; }


// USE THIS LATER FOR ORDER CREATIONS
        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     modelBuilder.Entity<Order>()
        //         .Property(b => b.DateCreated)
        //         .HasDefaultValueSql("strftime('%Y-%m-%d %H:%M:%S')");
        // }
    }
}