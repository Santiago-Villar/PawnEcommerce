using System;
using Moq;
using Service.Exception;
using Service.Product;
namespace Test
{
    [TestClass]
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

        [TestMethod]
        public void CanGetBrandById_Ok()
        {
            var brandRepository = new Mock<IBrandRepository>();
            var brandsList = new List<Brand>
            {
                new Brand(1) { Name = "Kova" },
                new Brand(2) { Name = "Brand2" },
                new Brand(3) { Name = "Brand3" }
            };
            brandRepository.Setup(repo => repo.GetById(It.IsAny<int>()))
                   .Returns<int>(id => brandsList.FirstOrDefault(b => b.Id == id));


            IBrandService service = new BrandService(brandRepository.Object);

            Brand brand1 = service.Get(1);
            Brand brand2 = service.Get(2);

            Assert.AreEqual(brand1.Name, brandsList[0].Name);
            Assert.AreEqual(brand1.Id, brandsList[0].Id);

            Assert.AreEqual(brand2.Name, brandsList[1].Name);
            Assert.AreEqual(brand2.Id, brandsList[1].Id);

        }

        [ExpectedException(typeof(RepositoryException))]
        [TestMethod]
        public void GetWithWrongId_Throw()
        {
            var brandRepository = new Mock<IBrandRepository>();
            brandRepository.Setup(repo => repo.GetById(It.IsAny<int>())).Returns((Brand) null);

            IBrandService service = new BrandService(brandRepository.Object);
            Brand b = service.Get(999);
        }


    }
}

