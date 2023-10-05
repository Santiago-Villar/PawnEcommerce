using System;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PawnEcommerce.Controllers;
using Service.Exception;
using Service.Product;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace Test.Controller
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class CategoryControllerTest
    {
        private Mock<ICategoryService> _categoryServiceMock;
        private CategoryController _categoryController;
        private List<Category> _categoriesList;

        [TestInitialize]
        public void SetUp()
        {
            _categoryServiceMock = new Mock<ICategoryService>();
            _categoryController = new CategoryController(_categoryServiceMock.Object);
            _categoriesList = new List<Category>
            {
                new Category(1) { Name = "Kova" },
                new Category(2) { Name = "Category2" },
                new Category(3) { Name = "Category3" }
            };
        }

        [TestMethod]
        public void CanCreateController_Ok()
        {
            Assert.IsNotNull(_categoryController);
        }

        [TestMethod]
        public void CanGetAllBrands_Ok()
        {
            _categoryServiceMock.Setup(service => service.GetAll()).Returns(_categoriesList);

            var result = _categoryController.GetAll() as OkObjectResult;
            var categories = result.Value as List<Category>;

            Assert.IsNotNull(result);
            Assert.AreEqual(categories.Count, 3);
            CollectionAssert.Contains(categories, _categoriesList[0]);
        }

        [TestMethod]
        public void CanGetBrandById_Ok()
        {
            _categoryServiceMock.Setup(service => service.Get(It.IsAny<int>()))
                   .Returns<int>(id => _categoriesList.FirstOrDefault(b => b.Id == id));

            var result1 = _categoryController.Get(1) as OkObjectResult;
            var category1 = result1.Value as Category;

            var result2 = _categoryController.Get(2) as OkObjectResult;
            var category2 = result2.Value as Category;

            Assert.AreEqual(category1.Name, _categoriesList[0].Name);
            Assert.AreEqual(category1.Id, _categoriesList[0].Id);

            Assert.AreEqual(category2.Name, _categoriesList[1].Name);
            Assert.AreEqual(category2.Id, _categoriesList[1].Id);
        }

        [ExpectedException(typeof(ModelException))]
        [TestMethod]
        public void GetBrandByNonExistentId_ReturnsNotFound()
        {
            _categoryServiceMock.Setup(service => service.Get(It.IsAny<int>())).Throws(new ModelException("Category not found"));
            var result = _categoryController.Get(999) as NotFoundObjectResult;
        }
    }
}
