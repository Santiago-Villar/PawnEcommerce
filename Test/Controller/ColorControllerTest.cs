using System;
using Microsoft.AspNetCore.Mvc;
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

        [TestMethod]
        public void CanGetAllColors_Ok()
        {
            var serviceMock = new Mock<IColorService>();
            var controller = new ColorController(serviceMock.Object);

            var colorsList = new List<Color>
            {
                new Color(1) { Name = "light Red", Code = "#2ASD2D" },
                new Color(2) { Name = "DARKY BLUEE", Code = "#AF1FFF"},
                new Color(3) { Name = "Dark asf", Code = "000000" }
            };
            serviceMock.Setup(repo => repo.GetAll()).Returns(colorsList);

            var result = controller.GetAll() as OkObjectResult;
            var colors = result.Value as List<Color>;

            Assert.AreEqual(colors.Count, 3);
            CollectionAssert.Contains(colors, colorsList[0]);
        }
    }

}

