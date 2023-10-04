using System;
using Moq;
using Service.Exception;
using Service.Product;

namespace Test
{
    [TestClass]
    public class CategoryServiceTest
	{
        private Mock<ICategoryRepository> _categoryRepositoryMock;
        private ICategoryService _service;
        private List<Category> _categoriesList;

        [TestInitialize]
        public void TestSetup()
        {
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _service = new CategoryService(_categoryRepositoryMock.Object);

            _categoriesList = new List<Category>
            {
                new Category(1) { Name = "Kova" },
                new Category(2) { Name = "Brand2" },
                new Category(3) { Name = "Brand3" }
            };
        }

        [TestMethod]
        public void CanCreateCategoryService_Ok()
        {
            Assert.IsNotNull(_service);
        }

        [TestMethod]
        public void CanGetAllCategories_Ok()
        {
            _categoryRepositoryMock.Setup(repo => repo.GetAll()).Returns(_categoriesList);

            List<Category> categories = _service.GetAll();

            Assert.AreEqual(categories.Count, 3);
            CollectionAssert.Contains(categories, _categoriesList[0]);
        }

        [TestMethod]
        public void CanGetCategoryById_Ok()
        {
            _categoryRepositoryMock.Setup(repo => repo.GetById(It.IsAny<int>()))
                   .Returns<int>(id => _categoriesList.FirstOrDefault(b => b.Id == id));

            Category category1 = _service.Get(1);
            Category category2 = _service.Get(2);

            Assert.AreEqual(category1.Name, _categoriesList[0].Name);
            Assert.AreEqual(category1.Id, _categoriesList[0].Id);

            Assert.AreEqual(category2.Name, _categoriesList[1].Name);
            Assert.AreEqual(category2.Id, _categoriesList[1].Id);
        }

        [ExpectedException(typeof(RepositoryException))]
        [TestMethod]
        public void GetWithWrongId_Throw()
        {
            _categoryRepositoryMock.Setup(repo => repo.GetById(It.IsAny<int>())).Returns((Category)null);
            Category c = _service.Get(999);
        }
    }
}

