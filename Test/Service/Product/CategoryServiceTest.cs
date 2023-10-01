using System;
using Moq;
using Service.Exception;
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
                   .Returns<int>(id => categoriesList.FirstOrDefault(b => b.Id == id));


            ICategoryService service = new CategoryService(categoryRepository.Object);

            Category category1 = service.Get(1);
            Category category2 = service.Get(2);

            Assert.AreEqual(category1.Name, categoriesList[0].Name);
            Assert.AreEqual(category1.Id, categoriesList[0].Id);

            Assert.AreEqual(category2.Name, categoriesList[1].Name);
            Assert.AreEqual(category2.Id, categoriesList[1].Id);
        }

        [ExpectedException(typeof(ModelException))]
        [TestMethod]
        public void GetWithWrongId_Throw()
        {
            var categoryRepository = new Mock<ICategoryRepository>();
            categoryRepository.Setup(repo => repo.GetById(It.IsAny<int>())).Returns((Category)null);

            ICategoryService service = new CategoryService(categoryRepository.Object);
            Category c = service.Get(999);
        }
    }
}

