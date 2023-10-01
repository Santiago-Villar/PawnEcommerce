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
    }
}

