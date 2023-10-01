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
                new Category(1) { Name = "Kova" },
                new Category(2) { Name = "Brand2" },
                new Category(3) { Name = "Brand3" }
            };
            categoryRepository.Setup(repo => repo.GetAll()).Returns(categoriesList);
            ICategoryService service = new CategoryService(categoryRepository.Object);

            List<Category> categories = service.GetAll();

            Assert.AreEqual(categories.Count, 3);
            CollectionAssert.Contains(categories, categoriesList[0]);
        }

        [TestMethod]
        public void CanGetCategoryById_Ok()
        {
            var categoryRepository = new Mock<ICategoryRepository>();
            var categoriesList = new List<Category>
            {
                new Category(1) { Name = "Kova" },
                new Category(2) { Name = "Brand2" },
                new Category(3) { Name = "Brand3" }
            };
            categoryRepository.Setup(repo => repo.GetById(It.IsAny<int>()))
                   .Returns<int>(id => brandsList.FirstOrDefault(b => b.Id == id));


            ICategoryService service = new CategoryService(categoryRepository.Object);

            Brand brand1 = service.Get(1);
            Brand brand2 = service.Get(2);

            Assert.AreEqual(brand1.Name, categoriesList[0].Name);
            Assert.AreEqual(brand1.Id, categoriesList[0].Id);

            Assert.AreEqual(brand2.Name, categoriesList[1].Name);
            Assert.AreEqual(brand2.Id, categoriesList[1].Id);
        }
    }
}

