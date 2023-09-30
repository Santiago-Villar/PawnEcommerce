using System;
using Moq;
using Service.Product;
namespace Test
{
	public class BrandServiceTest
	{
		public BrandServiceTest()
		{
		}

        [TestMethod]
        public void CanCreateBrandService_Ok()
        {
            var brandRepository = new Mock<IBrandRepository>().Object;
            IBrandService service = new BrandService(brandRepository);
            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void CanGetAllBrands_Ok()
        {
            var brandRepository = new Mock<IBrandRepository>();
            var brandsList = new List<Brand>
            {
                new Brand(1) { Name = "Kova" },
                new Brand(2) { Name = "Brand2" },
                new Brand(3) { Name = "Brand3" }
            };
            brandRepository.Setup(repo => repo.GetAll()).Returns(brandsList);


            IBrandService service = new BrandService(brandRepository.Object);
            List<Brand> brands = service.GetAll();
            Assert.AreEqual(brands.Count, 3);
            CollectionAssert.Contains(brands, brandsList[0]);
        }

    }
}

