using System;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PawnEcommerce.Controllers;
using Service.Product;

namespace Test.Controller
{
    [TestClass]
    public class CategoryControllerTest
	{
        [TestMethod]
        public void CanCreateController_Ok()
        {
           var categoryServiceMock = new Mock<ICategoryService>();
           var categoryController = new CategoryController(categoryServiceMock.Object);
           Assert.IsNotNull(categoryController);
        }

        [TestMethod]
        public void CanGetAllBrands_Ok()
        {
            var categoryServiceMock = new Mock<ICategoryService>();
            var categoryController = new CategoryController(categoryServiceMock.Object);
            var categoriesList = new List<Category>
            {
                new Category(1) { Name = "Kova" },
                new Category(2) { Name = "Category2" },
                new Category(3) { Name = "Category3" }
            };
            categoryServiceMock.Setup(service => service.GetAll()).Returns(categoriesList);

            var result = categoryController.GetAll() as OkObjectResult;
            var categories = result.Value as List<Category>;

            Assert.IsNotNull(result);
            Assert.AreEqual(categories.Count, 3);
            CollectionAssert.Contains(categories, categoriesList[0]);
        }

        [TestMethod]
        public void CanGetBrandById_Ok()
        {
            var categoryServiceMock = new Mock<ICategoryService>();
            var categoryController = new CategoryController(categoryServiceMock.Object);
            var categoriesList = new List<Category>
            {
                new Category(1) { Name = "Kova" },
                new Category(2) { Name = "Category2" },
                new Category(3) { Name = "Category3" }
            };

            categoryServiceMock.Setup(service => service.Get(It.IsAny<int>()))
                   .Returns<int>(id => categoriesList.FirstOrDefault(b => b.Id == id));

            var result1 = categoryController.Get(1) as OkObjectResult;
            var category1 = result1.Value as Category;

            var result2 = categoryController.Get(2) as OkObjectResult;
            var category2 = result2.Value as Category;

            Assert.AreEqual(category1.Name, categoriesList[0].Name);
            Assert.AreEqual(category1.Id, categoriesList[0].Id);

            Assert.AreEqual(category2.Name, categoriesList[1].Name);
            Assert.AreEqual(category2.Id, categoriesList[1].Id);
        }
    }
}

