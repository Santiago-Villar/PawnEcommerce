using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Service.Product;
using System.Linq;
using System.Collections.Generic;
using Service.Filter.ConcreteFilter;

namespace Test
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

            var (updatedCart, removedProducts) = _cartUtils.VerifyAndUpdateCart(new[] { cartProduct });

            Assert.IsFalse(updatedCart.Any());  // Cart should be empty.
            Assert.IsTrue(removedProducts.Contains(cartProduct));  // Product should be in the removed list.
        }

        [TestMethod]
        public void VerifyAndUpdateCart_SufficientStock_UpdatesCartWithLatestProductDetails()
        {
            var cartProduct = new Product { Id = 1 };
            var latestProduct = new Product { Id = 1 };
            latestProduct.Stock = 5;

            _mockProductService.Setup(s => s.GetAllProducts(It.IsAny<FilterQuery>())).Returns(new[] { latestProduct });

            var (updatedCart, removedProducts) = _cartUtils.VerifyAndUpdateCart(new[] { cartProduct });

            Assert.AreEqual(1, updatedCart.Length);
            Assert.AreSame(latestProduct, updatedCart[0]);  // Make sure it's the latest product version
            Assert.IsFalse(removedProducts.Any());  // Ensure no products were removed
        }

        [TestMethod]
        public void VerifyAndUpdateCart_InsufficientStock_RemovesProductFromCart()
        {
            var cartProduct = new Product { Id = 1 };
            var latestProduct = new Product { Id = 1 };
            latestProduct.Stock = 0;  // No stock available

            _mockProductService.Setup(s => s.GetAllProducts(It.IsAny<FilterQuery>())).Returns(new[] { latestProduct });

            var (updatedCart, removedProducts) = _cartUtils.VerifyAndUpdateCart(new[] { cartProduct });

            Assert.IsFalse(updatedCart.Any());  // Product should be removed from the cart
            Assert.IsTrue(removedProducts.Contains(cartProduct));  // Product should be in the removed list.
        }
    }
}


