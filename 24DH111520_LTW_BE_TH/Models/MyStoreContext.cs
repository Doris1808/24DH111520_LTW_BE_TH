using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace _24DH111520_LTW_BE_TH.Models;

public partial class MyStoreContext : DbContext
{
    public MyStoreContext()
    {
    }

    public MyStoreContext(DbContextOptions<MyStoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<Customer> Customers { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<OrderDetail> OrderDetails { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId);
            entity.ToTable("Category");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryId");
            entity.Property(e => e.CategoryName).HasColumnName("CategoryName");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId);
            entity.ToTable("Customer");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerId");
            entity.Property(e => e.CustomerAddress).HasColumnName("CustomerAddress");
            entity.Property(e => e.CustomerEmail).HasColumnName("CustomerEmail");
            entity.Property(e => e.CustomerName).HasColumnName("CustomerName");
            entity.Property(e => e.CustomerPhone).HasMaxLength(15).HasColumnName("CustomerPhone");
            entity.Property(e => e.Username).HasMaxLength(255).HasColumnName("Username");

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.Customers)
                .HasForeignKey(d => d.Username)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId);
            entity.ToTable("Order");
            entity.Property(e => e.OrderId).HasColumnName("OrderId");
            entity.Property(e => e.AddressDelivery).HasColumnName("AddressDelivery");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerId");
            entity.Property(e => e.OrderDate).HasColumnName("OrderDate").HasColumnType("timestamp");
            entity.Property(e => e.PaymentStatus).HasColumnName("PaymentStatus");
            entity.Property(e => e.TotalAmount).HasPrecision(18, 2).HasColumnName("TotalAmount");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("OrderDetail");
            entity.Property(e => e.Id).HasColumnName("Id");
            entity.Property(e => e.OrderId).HasColumnName("OrderId");
            entity.Property(e => e.ProductId).HasColumnName("ProductId");
            entity.Property(e => e.Quantity).HasColumnName("Quantity");
            entity.Property(e => e.UnitPrice).HasPrecision(18, 2).HasColumnName("UnitPrice");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId);
            entity.ToTable("Product");
            entity.Property(e => e.ProductId).HasColumnName("ProductId");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryId");
            entity.Property(e => e.ProductDescription).HasColumnName("ProductDescription");
            entity.Property(e => e.ProductImage).HasColumnName("ProductImage");
            entity.Property(e => e.ProductName).HasColumnName("ProductName");
            entity.Property(e => e.ProductPrice).HasPrecision(18, 2).HasColumnName("ProductPrice");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Username);
            entity.ToTable("User");
            entity.Property(e => e.Username).HasMaxLength(255).HasColumnName("Username");
            entity.Property(e => e.Password).HasMaxLength(50).HasColumnName("Password");
            entity.Property(e => e.UserRole).HasMaxLength(1).HasColumnName("UserRole");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
