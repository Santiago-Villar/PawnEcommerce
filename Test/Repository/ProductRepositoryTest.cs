﻿using Microsoft.EntityFrameworkCore;
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
            var brandName = "Sample Brand";
            var categoryName = "Sample Category";

            var brand = context.Brands.SingleOrDefault(b => b.Name == brandName);
            if (brand == null)
            {
                brand = new Brand { Name = brandName };
                context.Brands.Add(brand);
            }

            var category = context.Categories.SingleOrDefault(c => c.Name == categoryName);
            if (category == null)
            {
                category = new Category { Name = categoryName };
                context.Categories.Add(category);
            }

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
            Assert.IsFalse(exists);
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


    }
}


