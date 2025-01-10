using Domain_Layer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure_Layer.Database
{
    public class DatabaseContext : IdentityDbContext<User>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<ClothingType> ClothingTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne()
                .HasForeignKey(oi => oi.OrderId);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.ClothingTypes)
                .WithOne(ct => ct.Category)
                .HasForeignKey(ct => ct.CategoryId);

            modelBuilder.Entity<ClothingType>()
                .HasMany(ct => ct.Products)
                .WithOne(p => p.ClothingType)
                .HasForeignKey(p => p.ClothingTypeId);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Cart)
                .WithOne()
                .HasForeignKey<Cart>(c => c.UserId); 
     
            modelBuilder.Entity<Cart>()
                 .HasMany(c => c.Items)
                 .WithOne()
                 .HasForeignKey(ci => ci.CartId);

            modelBuilder.Entity<CartItem>()
                .HasOne(c => c.Product)
                .WithMany(p => p.CartItems)
                .HasForeignKey(c => c.ProductId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductId);
           
            modelBuilder.Entity<Product>()
                .HasMany(p => p.CartItems)
                .WithOne(ci => ci.Product)
                .HasForeignKey(ci => ci.ProductId);

            modelBuilder.Entity<CartItem>()
                .Property(c => c.TotalPrice)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<CartItem>()
                .Property(c => c.UnitPrice)
                .HasColumnType("decimal(18, 2)");
            
            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.UnitPrice)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18, 2)");


        }
    }
}
