using Microsoft.EntityFrameworkCore;
using Repository;
using Service.Exception;
using Service.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Repository
{
    [TestClass]
    public class ColorRepositoryTest
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

        private Color CreateSampleColor(EcommerceContext context)
        {
            var color = new Color(1)
            {
                Name = "Sample Color",
                Code = "code"
            };
            context.Colors.Add(color);
            context.SaveChanges();

            return color;
        }

        private Color CreateSampleColor2(EcommerceContext context)
        {
            var color = new Color(2)
            {
                Name = "Sample Color 2",
                Code = "code 2"
            };
            context.Colors.Add(color);
            context.SaveChanges();

            return color;
        }

        [TestMethod]
        public void GetAll_Ok()
        {
            using var context = GetInMemoryDbContext();
            var repository = new ColorRepository(context);

            var color = CreateSampleColor(context);
            var color2 = CreateSampleColor2(context);

            context.Colors.Add(color);
            context.Colors.Add(color2);

            var colors = repository.GetAll();
            Assert.AreEqual(2, colors.Count);
        }

        [TestMethod]
        public void GetById_Ok()
        {
            using var context = GetInMemoryDbContext();
            var repository = new ColorRepository(context);

            var color = CreateSampleColor(context);

            var fetchedColor = repository.GetById(color.Id);

            Assert.IsNotNull(fetchedColor);
            Assert.AreEqual("Sample Color", fetchedColor.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ModelException))]
        public void GetById_NoColorFound_Throw()
        {
            using var context = GetInMemoryDbContext();
            var repository = new ColorRepository(context);

            repository.GetById(999);
        }
    }
}

