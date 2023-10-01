using System;
using Moq;
using Service.Product;

namespace Test
{
    [TestClass]
    public class CategoryServiceTest
	{
		public CategoryServiceTest()
		{
		}

        [TestMethod]
        public void CanCreateBrandService_Ok()
        {
            var categoryRepository = new Mock<ICategoryRepository>().Object;
            ICategoryService service = new CategoryService(categoryRepository);
            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void CanGetAllCategories_Ok()
        {
            var categoryRepository = new Mock<ICategoryRepository>();
            var categoriesList = new List<Category>
            {
                new Category() { Name = "Kova" },
                new Category() { Name = "Brand2" },
                new Category() { Name = "Brand3" }
            };
            categoryRepository.Setup(repo => repo.GetAll()).Returns(categoriesList);
            ICategoryService service = new CategoryService(categoryRepository.Object);

            List<Brand> categories = service.GetAll();

            Assert.AreEqual(categories.Count, 3);
            CollectionAssert.Contains(categories, categoriesList[0]);
        }
    }
}

