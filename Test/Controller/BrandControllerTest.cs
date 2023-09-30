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

        [TestMethod]
        public void CanGetAllBrands_Ok()
        {
            Mock<IBrandService> userServiceMock = new Mock<IBrandService>();
            var brandsList = new List<Brand>
            {
                new Brand(1) { Name = "Kova" },
                new Brand(2) { Name = "Brand2" },
                new Brand(3) { Name = "Brand3" }
            };
            userServiceMock.Setup(service => service.GetAll()).Returns(brandsList);

            var brandController = new BrandController(userServiceMock.Object);
            List<Brand> brands = brandController.GetAll();

            Assert.AreEqual(brands.Count, 3);
            CollectionAssert.Contains(brands, brandsList[0]);
        }
    }
}

