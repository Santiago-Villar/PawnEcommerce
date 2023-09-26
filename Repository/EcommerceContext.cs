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
        public DbSet<Color> Colors { get; set; } // Added DbSet for Color.

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Ensure uniqueness for the Name attributes of Brand and Category.
            modelBuilder.Entity<Brand>()
                .HasIndex(b => b.Name)
                .IsUnique();

            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Name)
                .IsUnique();

            // Configure Product
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Name)
                .IsUnique();  // Ensure Product Name uniqueness

            // Define relationships:

            // Product to Colors
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Colors)
                .WithOne(c => c.Product)
                .HasForeignKey(c => c.ProductId); // Assuming you add a ProductId foreign key to the Color model.

            // Product to Brand
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Brand)
                .WithMany(b => b.Products)
                .HasForeignKey(p => p.BrandName);

            // Product to Category
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryName);

            // Add other configurations as needed.

            // Configure User
            modelBuilder.Entity<User>()
                .HasKey(u => u.Email);  // Set Email as primary key for User
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();  // Ensure Email uniqueness

            modelBuilder.Entity<User>()
                .Property(u => u.Roles)
                .HasConversion(
                                roles => string.Join(",", roles.Select(r => r.ToString())),
                                rolesString => rolesString.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                                            .Select(r => Enum.Parse<RoleType>(r))
                                                            .ToList()
                               );

            // Configure Sale
            modelBuilder.Entity<Sale>()
                .HasKey(s => s.Id);  // Define primary key for Sale

            modelBuilder.Entity<Sale>()
                .Property(s => s.Date)
                .HasDefaultValueSql("getdate()"); // Default value for DateTime.Now. Assumes SQL Server.

            modelBuilder.Entity<Sale>()
                .HasOne(s => s.User)
                .WithMany(u => u.Sales) // Assuming no navigation property on User for sales.
                .HasForeignKey(s => s.UserEmail); // Using Email as foreign key as per earlier discussion.

            // Configure SaleProduct
            modelBuilder.Entity<SaleProduct>()
                .HasKey(sp => new { sp.SaleId, sp.ProductId }); // Composite primary key for SaleProduct

            modelBuilder.Entity<SaleProduct>()
                .HasOne(sp => sp.Sale)
                .WithMany(s => s.Products)  // The collection of SaleProducts on Sale
                .HasForeignKey(sp => sp.SaleId);

            modelBuilder.Entity<SaleProduct>()
                .HasOne(sp => sp.Product)
                .WithMany() // Assuming no navigation property on Product for SaleProduct
                .HasForeignKey(sp => sp.ProductId);


        }

    }

}