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
        private Mock<IColorService> _colorServiceMock;
        private ColorController _colorController;
        private List<Color> _colorsList;

        [TestInitialize]
        public void SetUp()
        {
            _colorServiceMock = new Mock<IColorService>();
            _colorController = new ColorController(_colorServiceMock.Object);

            _colorsList = new List<Color>
            {
                new Color(1) { Name = "light Red", Code = "#2ASD2D" },
                new Color(2) { Name = "DARKY BLUEE", Code = "#AF1FFF"},
                new Color(3) { Name = "Dark asf", Code = "000000" }
            };
        }

        [TestMethod]
        public void CanCreateController_Ok()
        {
            Assert.IsNotNull(_colorController);
        }

        [TestMethod]
        public void CanGetAllColors_Ok()
        {
            _colorServiceMock.Setup(repo => repo.GetAll()).Returns(_colorsList);

            var result = _colorController.GetAll() as OkObjectResult;
            var colors = result.Value as List<Color>;

            Assert.AreEqual(colors.Count, 3);
            CollectionAssert.Contains(colors, _colorsList[0]);
        }

        [TestMethod]
        public void CanGetColorById_Ok()
        {
            _colorServiceMock.Setup(service => service.Get(It.IsAny<int>()))
                   .Returns<int>(id => _colorsList.FirstOrDefault(b => b.Id == id));

            var result1 = _colorController.Get(1) as OkObjectResult;
            var color1 = result1.Value as Color;

            var result2 = _colorController.Get(2) as OkObjectResult;
            var color2 = result2.Value as Color;

            Assert.AreEqual(color1.Name, _colorsList[0].Name);
            Assert.AreEqual(color1.Id, _colorsList[0].Id);

            Assert.AreEqual(color2.Name, _colorsList[1].Name);
            Assert.AreEqual(color2.Id, _colorsList[1].Id);
        }
    }

}

