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
            ICategoryService service = new CategoryService(categoryRepository);
            Assert.IsNotNull(service);
        }
    }
}

