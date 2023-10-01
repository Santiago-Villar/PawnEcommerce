using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;
using Service.Product;
using System.Linq;

namespace Test
{
    [TestClass]
    public class CategoryRepositoryTests
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

        private Category CreateSampleCategory(EcommerceContext context)
        {
            var category = new Category
            {
                Id=1,
                Name = "Sample Category"
            };
            context.Categories.Add(category);
            context.SaveChanges();

            return category;
        }

        [TestMethod]
        public void GetAll_ShouldReturnAllCategories()
        {
            using var context = GetInMemoryDbContext();
            var repository = new CategoryRepository(context);

            var category1 = CreateSampleCategory(context);

            var category2 = new Category
            {
                Id=2,
                Name = "Another Sample Category"
            };
            context.Categories.Add(category2);
            context.SaveChanges();

            var categories = repository.GetAll();
            Assert.AreEqual(2, categories.Count);
        }

        [TestMethod]
        public void GetById_ShouldReturnCorrectCategory()
        {
            using var context = GetInMemoryDbContext();
            var repository = new CategoryRepository(context);

            var category = CreateSampleCategory(context);

            var fetchedCategory = repository.GetById(category.Id.Value);
            Assert.IsNotNull(fetchedCategory);
            Assert.AreEqual("Sample Category", fetchedCategory.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetById_ShouldThrowException_WhenCategoryNotFound()
        {
            using var context = GetInMemoryDbContext();
            var repository = new CategoryRepository(context);

            repository.GetById(999);  // We are assuming that this ID doesn't exist
        }
    }
}

