using System;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PawnEcommerce.Controllers;
using Service.Product;

namespace Test.Controller
{
	[TestClass]
	public class BrandControllerTest
	{

        private Mock<IBrandService> _brandServiceMock;
        private BrandController _brandController;
        private List<Brand> _brandsList;

        [TestInitialize]
        public void SetUp()
        {
            _brandServiceMock = new Mock<IBrandService>();
            _brandController = new BrandController(_brandServiceMock.Object);

            _brandsList = new List<Brand>
            {
                new Brand(1) { Name = "Kova" },
                new Brand(2) { Name = "Brand2" },
                new Brand(3) { Name = "Brand3" }
            };
        }

        [TestMethod]
        public void CanCreateController_Ok()
        {
            Assert.IsNotNull(_brandController);
        }

        [TestMethod]
        public void CanGetAllBrands_Ok()
        {
            _brandServiceMock.Setup(service => service.GetAll()).Returns(_brandsList);

            var result = _brandController.GetAll() as OkObjectResult;
            var brands = result.Value as List<Brand>;

            Assert.AreEqual(brands.Count, 3);
            CollectionAssert.Contains(brands, _brandsList[0]);
        }

        [TestMethod]
        public void CanGetBrandById_Ok()
        {
            _brandServiceMock.Setup(service => service.Get(It.IsAny<int>()))
                   .Returns<int>(id => _brandsList.FirstOrDefault(b => b.Id == id));

            var result1 = _brandController.Get(1) as OkObjectResult;
            var brand1 = result1.Value as Brand;

            var result2 = _brandController.Get(2) as OkObjectResult;
            var brand2 = result2.Value as Brand;

            Assert.AreEqual(brand1.Name, _brandsList[0].Name);
            Assert.AreEqual(brand1.Id, _brandsList[0].Id);

            Assert.AreEqual(brand2.Name, _brandsList[1].Name);
            Assert.AreEqual(brand2.Id, _brandsList[1].Id);
        }
    }
}

