﻿using Microsoft.EntityFrameworkCore;
using Service.Product;
using Service.User;
using Service.Sale;
using Service.User.Role;
namespace Repository
{

    public class EcommerceContext : DbContext
    {
        public EcommerceContext(DbContextOptions<EcommerceContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductColor> ProductColors { get; set; }


        public DbSet<SaleProduct> SaleProducts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Color> Colors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>()
                .HasKey(b => b.Id);

            modelBuilder.Entity<Category>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Name)
                .IsUnique();

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Brand)
                .WithMany(b => b.Products)
                .HasForeignKey(p => p.BrandId);

            modelBuilder.Entity<Product>()
                 .Property(p => p.Stock)
                 .HasDefaultValue(0);


            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<ProductColor>()
                .HasKey(pc => new { pc.ProductId, pc.ColorId });

            modelBuilder.Entity<ProductColor>()
                .HasOne(pc => pc.Product)
                .WithMany(p => p.ProductColors)
                .HasForeignKey(pc => pc.ProductId);

            modelBuilder.Entity<ProductColor>()
                .HasOne(pc => pc.Color)
                .WithMany(c => c.ProductColors)
                .HasForeignKey(pc => pc.ColorId);

            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .Property(u => u.Roles)
                .HasConversion(
                    roles => string.Join(",", roles.Select(r => r.ToString())),
                    rolesString => rolesString.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                              .Select(r => Enum.Parse<RoleType>(r))
                                              .ToList());

            modelBuilder.Entity<Sale>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<Sale>()
                  .HasOne(s => s.User)
                  .WithMany(u => u.Sales)
                  .HasForeignKey(s => s.UserId);


            modelBuilder.Entity<Sale>()
                .Property(s => s.Date)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Sale>()
                .Property(s => s.PromotionName)
                .IsRequired(false);

            modelBuilder.Entity<Sale>()
                .Property(s => s.Price)
                .IsRequired(false);

            modelBuilder.Entity<Sale>()
                .Property(s => s.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<SaleProduct>()
                .HasKey(sp => sp.Id);

            modelBuilder.Entity<SaleProduct>()
                .Property(sp => sp.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<SaleProduct>()
                .HasOne(sp => sp.Sale)
                .WithMany(s => s.Products)
                .HasForeignKey(sp => sp.SaleId);

            modelBuilder.Entity<SaleProduct>()
                .HasOne(sp => sp.Product)
                .WithMany()
                .HasForeignKey(sp => sp.ProductId);
        }


    }

}