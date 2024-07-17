using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Domain.Entities;
using OrderManagementSystem.Domain.Entities.Identity;
using System.Reflection;

namespace OrderManagementSystem.Infrastructure.Databases
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<User>(options)
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Customer>().Property(b => b.Id).HasColumnName("CustomerId");
            builder.Entity<Invoice>().Property(b => b.Id).HasColumnName("InvoiceId");
            builder.Entity<Order>().Property(b => b.Id).HasColumnName("OrderId");
            builder.Entity<OrderItem>().Property(b => b.Id).HasColumnName("OrderItemId");
            builder.Entity<Product>().Property(b => b.Id).HasColumnName("ProductId");
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            // To make All Relationships OnDelete : Restrict
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
