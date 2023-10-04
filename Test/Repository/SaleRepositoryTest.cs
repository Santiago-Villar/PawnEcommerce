using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;
using Service.Sale;
using Service.User;
using System;
using System.Linq;

namespace Test
{
    [TestClass]
    public class SaleRepositoryTests
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

        private Sale CreateSampleSale(EcommerceContext context)
        {
            var user = new User
            {
                Email = "testuser@email.com",
                Address = "123 Main St, City, Country",
                PasswordHash = "sampleHashedPassword123"
                                                         
            };
            context.Users.Add(user);
            context.SaveChanges();

            return new Sale
            {
                Price = 100.0,
                PromotionName = "Sample Promotion"
            };
        }
        private Sale CreateAnotherSampleSale(EcommerceContext context)
        {
            return new Sale
            {
                Price = 200.0,
                PromotionName = "Another Sample Promotion"
            };
        }


        [TestMethod]
        public void Add_ShouldWork()
        {
            using var context = GetInMemoryDbContext();
            var repository = new SaleRepository(context);

            var sale = CreateSampleSale(context);
            repository.Add(sale);

            var saleInDb = context.Sales.FirstOrDefault(s => s.Id == sale.Id);
            Assert.IsNotNull(saleInDb);
            Assert.AreEqual(100.0, saleInDb.Price);
        }


        [TestMethod]
        public void GetAll_ShouldReturnAllSales()
        {
            using var context = GetInMemoryDbContext();
            var repository = new SaleRepository(context);

            var sale1 = CreateSampleSale(context);
            var sale2 = CreateAnotherSampleSale(context);
            context.Sales.Add(sale1);
            context.Sales.Add(sale2);
            context.SaveChanges();

            var sales = repository.GetAll();
            Assert.AreEqual(2, sales.Count);
        }
    }
}

