using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Service.Product;
using System.Linq;
using System.Collections.Generic;
using Service.Filter.ConcreteFilter;

namespace Test.Utilities
{
    [TestClass]
    public class CartUtilsTest
    {
        private Mock<IProductService> _mockProductService;
        private CartUtils _cartUtils;

        [TestInitialize]
        public void Setup()
        {
            _mockProductService = new Mock<IProductService>();
            _cartUtils = new CartUtils(_mockProductService.Object);
        }

        [TestMethod]
        public void VerifyAndUpdateCart_ProductNoLongerAvailable_RemovesFromCart()
        {
            var cartProduct = new Product { Id = 1 };
            _mockProductService.Setup(s => s.GetAllProducts(It.IsAny<FilterQuery>())).Returns(new Product[] { });

            var result = CartUtils.VerifyAndUpdateCart(new[] { cartProduct });

            Assert.IsFalse(result.Any());  // Cart should be empty.
        }

        [TestMethod]
        public void VerifyAndUpdateCart_SufficientStock_UpdatesCartWithLatestProductDetails()

    }
}

