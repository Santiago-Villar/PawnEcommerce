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

        [TestMethod]
        public void CanGetBrandById_Ok()
        {
            Mock<IBrandService> userServiceMock = new Mock<IBrandService>();
            var brandsList = new List<Brand>
            {
                new Brand(1) { Name = "Kova" },
                new Brand(2) { Name = "Brand2" },
                new Brand(3) { Name = "Brand3" }
            };
            userServiceMock.Setup(service => service.Get(It.IsAny<int>()))
                   .Returns<int>(id => brandsList.FirstOrDefault(b => b.Id == id));

            var brandController = new BrandController(userServiceMock.Object);

            Brand brand1 = brandController.Get(1);
            Brand brand2 = brandController.Get(2);

            Assert.AreEqual(brand1.Name, brandsList[0].Name);
            Assert.AreEqual(brand1.Id, brandsList[0].Id);

            Assert.AreEqual(brand2.Name, brandsList[1].Name);
            Assert.AreEqual(brand2.Id, brandsList[1].Id);
        }
    }
}

