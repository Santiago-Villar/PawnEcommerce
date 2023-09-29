using Microsoft.EntityFrameworkCore;
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

        public DbSet<SaleProduct> SaleProducts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Color> Colors { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Brand>()
                .HasIndex(b => b.Name)
                .IsUnique();

            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Name)
                .IsUnique();

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Name)
                .IsUnique();  


            modelBuilder.Entity<Product>()
                .HasMany(p => p.Colors)
                .WithOne(c => c.Product)
                .HasForeignKey(c => c.ProductId); 

            
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Brand)
                .WithMany(b => b.Products)
                .HasForeignKey(p => p.BrandName);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryName);


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
                                                            .ToList()
                               );

            modelBuilder.Entity<Sale>()
                .HasKey(s => s.Id);  

            modelBuilder.Entity<Sale>()
                .Property(s => s.Date)
                .HasDefaultValueSql("getdate()"); 

            modelBuilder.Entity<Sale>()
                .HasOne(s => s.User)
                .WithMany(u => u.Sales) 
                .HasForeignKey(s => s.UserId); 

            modelBuilder.Entity<SaleProduct>()
                .HasKey(sp => new { sp.SaleId, sp.ProductId }); 

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