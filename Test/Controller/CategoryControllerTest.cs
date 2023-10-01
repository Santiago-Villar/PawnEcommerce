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
    }
}

