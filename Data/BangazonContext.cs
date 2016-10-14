using Microsoft.EntityFrameworkCore;
using Bangazon.Models;

namespace Bangazon.Data
{
    public class BangazonContext : DbContext
    {
        public BangazonContext(DbContextOptions<BangazonContext> options)
            : base(options)
        { }
//DbSet: Database set (these are the tables we want to interact with..)
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<PaymentType> PaymentType { get; set; }
        public DbSet<LineItem> LineItem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .Property(b => b.DateCreated)
                .HasDefaultValueSql("strftime('%Y-%m-%d %H:%M:%S')");

            modelBuilder.Entity<Order>()
                .Property(b => b.DateCreated)
                .HasDefaultValueSql("strftime('%Y-%m-%d %H:%M:%S')");
//this tells the complier to calcuate this date and put it in the DateCreated and one other date field//
            modelBuilder.Entity<PaymentType>()
                .Property(b => b.DateCreated)
                .HasDefaultValueSql("strftime('%Y-%m-%d %H:%M:%S')");
        }
    }

}