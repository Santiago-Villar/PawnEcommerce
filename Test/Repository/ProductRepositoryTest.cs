using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;
using Service.Product;
using System.Linq;

namespace Test
{
    [TestClass]
    public class ProductRepositoryTests
    {
        private EcommerceContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<EcommerceContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
                .Options;

            var context = new EcommerceContext(options);
            context.Database.EnsureDeleted();
            return context;
        }

        private Product CreateSampleProduct(EcommerceContext context)
        {
            var brand = new Brand { Name = "Sample Brand" };
            var category = new Category { Name = "Sample Category" };

            context.Brands.Add(brand);
            context.Categories.Add(category);
            context.SaveChanges();

            return new Product
            {
                Name = "Sample Product",
                Description = "Sample Description",
                Price = 10,
                BrandName = brand.Name,
                CategoryName = category.Name
            };
        }

        [TestMethod]
        public void AddProduct_ShouldWork()
        {
            using var context = GetInMemoryDbContext();
            var repository = new ProductRepository(context);

            var product = CreateSampleProduct(context);
            repository.AddProduct(product);

            var productInDb = context.Products.FirstOrDefault(p => p.Name == "Sample Product");
            Assert.IsNotNull(productInDb);
            Assert.AreEqual("Sample Description", productInDb.Description);
        }

        [TestMethod]
        public void DeleteProduct_ShouldWork()
        {
            using var context = GetInMemoryDbContext();
            var repository = new ProductRepository(context);

            var product = CreateSampleProduct(context);
            context.Products.Add(product);
            context.SaveChanges();

            repository.DeleteProduct(product);

            var productInDb = context.Products.FirstOrDefault(p => p.Name == "Sample Product");
            Assert.IsNull(productInDb);
        }

        [TestMethod]
        public void Exists_ShouldReturnTrue_WhenProductExists()
        {
            using var context = GetInMemoryDbContext();
            var repository = new ProductRepository(context);

            var product = CreateSampleProduct(context);
            context.Products.Add(product);
            context.SaveChanges();

            var exists = repository.Exists(product);
            Assert.IsTrue(exists);
        }

        [TestMethod]
        public void Exists_ShouldReturnFalse_WhenProductDoesNotExist()
        {
            using var context = GetInMemoryDbContext();
            var repository = new ProductRepository(context);

            var product = CreateSampleProduct(context);

            var exists = repository.Exists(product);
            Assert.IsFalse(exists);  // Because we haven't added the product to the context.
        }

        [TestMethod]
        public void GetAllProducts_ShouldReturnAllProducts()
        {
            using var context = GetInMemoryDbContext();
            var repository = new ProductRepository(context);

            var product1 = CreateSampleProduct(context);
            var product2 = CreateSampleProduct(context);
            product2.Name = "Another Sample Product";  // Make sure the second product has a different name

            context.Products.AddRange(product1, product2);
            context.SaveChanges();

            var products = repository.GetAllProducts();
            Assert.AreEqual(2, products.Length);
        }

        [TestMethod]
        public void GetProductByName_ShouldReturnCorrectProduct()
        {
            using var context = GetInMemoryDbContext();
            var repository = new ProductRepository(context);

            var product = CreateSampleProduct(context);
            context.Products.Add(product);
            context.SaveChanges();

            var fetchedProduct = repository.GetProductByName("Sample Product");
            Assert.IsNotNull(fetchedProduct);
            Assert.AreEqual("Sample Description", fetchedProduct.Description);
        }

        [TestMethod]
        public void Reset_ShouldRemoveAllProducts()
        {
            using var context = GetInMemoryDbContext();
            var repository = new ProductRepository(context);

            var product1 = CreateSampleProduct(context);
            var product2 = CreateSampleProduct(context);
            product2.Name = "Another Sample Product";

            context.Products.AddRange(product1, product2);
            context.SaveChanges();

            repository.Reset();
            var productsCount = context.Products.Count();
            Assert.AreEqual(0, productsCount);
        }

        [TestMethod]
        public void UpdateProduct_ShouldUpdateExistingProduct()
        {
            using var context = GetInMemoryDbContext();
            var repository = new ProductRepository(context);

            var product = CreateSampleProduct(context);
            context.Products.Add(product);
            context.SaveChanges();

            product.Description = "Updated Description";
            repository.UpdateProduct(product);

            var updatedProduct = context.Products.FirstOrDefault(p => p.Name == "Sample Product");
            Assert.IsNotNull(updatedProduct);
            Assert.AreEqual("Updated Description", updatedProduct.Description);
        }
    }
}


