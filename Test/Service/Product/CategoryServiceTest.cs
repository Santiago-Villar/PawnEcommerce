using System;
using Moq;
using Service.Product;

namespace Test
{
    [TestClass]
    public class CategoryServiceTest
	{
		public CategoryServiceTest()
		{
		}

        [TestMethod]
        public void CanCreateBrandService_Ok()
        {
            var categoryRepository = new Mock<ICategoryRepository>().Object;
            IBrandService service = new BrandService(categoryRepository);
            Assert.IsNotNull(service);
        }
    }
}

