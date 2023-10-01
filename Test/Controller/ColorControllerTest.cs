using System;
using Moq;
using PawnEcommerce.Controllers;
using Service.Product;

namespace Test.Controller
{
    [TestClass]
    public class ColorControllerTest
	{
        [TestMethod]
        public void CanCreateController_Ok()
        {
            var serviceMock = new Mock<IColorService>();
            var controller = new ColorController(serviceMock.Object);
            Assert.IsNotNull(controller);
        }
    }
}

