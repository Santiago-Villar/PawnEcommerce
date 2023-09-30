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
            var brandRepository = new Mock<IBrandRepository>();
            IBrandService service = new BrandService();
        }
    }
}

