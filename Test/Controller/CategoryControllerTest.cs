using System;
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
    }
}

