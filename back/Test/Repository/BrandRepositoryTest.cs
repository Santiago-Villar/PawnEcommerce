﻿using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;
using Service.Exception;
using Service.Product;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace Test
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class BrandRepositoryTests
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

        private Brand CreateSampleBrand(EcommerceContext context)
        {
            var brand = new Brand()
            {
                Id=1,
                Name = "Sample Brand"
            };
            context.Brands.Add(brand);
            context.SaveChanges();

            return brand;
        }

        [TestMethod]
        public void GetAll_ShouldReturnAllBrands()
        {
            using var context = GetInMemoryDbContext();
            var repository = new BrandRepository(context);

            var brand1 = CreateSampleBrand(context); 

            var brand2 = new Brand
            {
                Id = 2,
                Name = "Another Sample Brand"
            };
            context.Brands.Add(brand2);
            context.SaveChanges();

            var brands = repository.GetAll();
            Assert.AreEqual(2, brands.Count);
        }


        [TestMethod]
        public void GetById_ShouldReturnCorrectBrand()
        {
            using var context = GetInMemoryDbContext();
            var repository = new BrandRepository(context);

            var brand = CreateSampleBrand(context);

            var fetchedBrand = repository.GetById(brand.Id);
            Assert.IsNotNull(fetchedBrand);
            Assert.AreEqual("Sample Brand", fetchedBrand.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(RepositoryException))]
        public void GetById_ShouldThrowException_WhenBrandNotFound()
        {
            using var context = GetInMemoryDbContext();
            var repository = new BrandRepository(context);

            repository.GetById(999);  
        }
    }
}

