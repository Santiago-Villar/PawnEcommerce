using System;
using Moq;
using PawnEcommerce.Controllers;
using Service.Product;

namespace Test.Controller
{
	[TestClass]
	public class BrandControllerTest
	{
		public BrandControllerTest()
		{
		}

        [TestMethod]
        public void CanCreateController_Ok()
        {
            Mock<IBrandService> userServiceMock = new Mock<IBrandService>();
            var brandController = new BrandController(userServiceMock.Object);
            Assert.IsNotNull(brandController);
        }
    }
}

