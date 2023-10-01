using System;
using Moq;
using Service.Product;

namespace Test
{
    [TestClass]
    public class ColorServiceTest
	{
        [TestMethod]
        public void CanCreateColorService_Ok()
        {
            var colorRepository = new Mock<IColorRepository>().Object;
            IColorService service = new ColorService(colorRepository);
            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void CanGetAllColors_Ok()
        {
            var colorsRepository = new Mock<IColorRepository>();
            var colorsList = new List<Color>
            {
                new Color(1) { Name = "light Red", Code = "#2ASD2D" },
                new Color(2) { Name = "DARKY BLUEE", Code = "#AF1FFF"},
                new Color(3) { Name = "Dark asf", Code = "000000" }
            };
            colorsRepository.Setup(repo => repo.GetAll()).Returns(colorsList);


            IColorService service = new ColorService(colorsRepository.Object);
            List<Color> colors = service.GetAll();
            Assert.AreEqual(colors.Count, 3);
            CollectionAssert.Contains(colors, colorsList[0]);
        }
    }
}

