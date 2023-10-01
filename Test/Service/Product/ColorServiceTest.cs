using System;
using Moq;
using Service.Exception;
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

        [TestMethod]
        public void CanGetBrandById_Ok()
        {
            var colorsRepository = new Mock<IColorRepository>();
            var colorsList = new List<Color>
            {
                new Color(1) { Name = "light Red", Code = "#2ASD2D" },
                new Color(2) { Name = "DARKY BLUEE", Code = "#AF1FFF"},
                new Color(3) { Name = "Dark asf", Code = "000000" }
            };
            colorsRepository.Setup(repo => repo.GetById(It.IsAny<int>()))
                   .Returns<int>(id => colorsList.FirstOrDefault(b => b.Id == id));


            IColorService service = new ColorService(colorsRepository.Object);

            Color color1 = service.Get(1);
            Color color2 = service.Get(2);

            Assert.AreEqual(color1.Name, colorsList[0].Name);
            Assert.AreEqual(color1.Id, colorsList[0].Id);

            Assert.AreEqual(color2.Name, colorsList[1].Name);
            Assert.AreEqual(color2.Id, colorsList[1].Id);

        }

        [ExpectedException(typeof(ModelException))]
        [TestMethod]
        public void GetWithWrongId_Throw()
        {
            var colorsRepository = new Mock<IColorRepository>();
            colorsRepository.Setup(repo => repo.GetById(It.IsAny<int>())).Returns((Color)null);

            IColorService service = new ColorService(colorsRepository.Object);
            Color b = service.Get(999);
        }
    }
}

