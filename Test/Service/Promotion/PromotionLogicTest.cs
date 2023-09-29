using System;
using Service.Exception;
using Service.Product;
using Service.Promotion;
using Moq;

namespace Test.Service.Promotion
{
    [TestClass]
    public class PromotionLogicTest
	{
        IPromotionService Logic = new PromotionService();


        [TestMethod]
        public void CanCreatePromotionsLogic_Ok()
        {
            Assert.IsNotNull(Logic);
        }

        [ExpectedException(typeof(ServiceException))]
        [TestMethod]
        public void GetPromotionWithNoProductsThrowsException()
        {
            List<Product> products = new List<Product>();

            IPromotionStrategy promo = Logic.GetPromotion(products);
        }

        [TestMethod]
        public void GetPromotion_Ok()
        {
            Product p1 = new Product() { };
            Product p2 = new Product() { };
            List<Product> products = new List<Product> { p1, p2 };

            IPromotionStrategy promo = Logic.GetPromotion(products);

            Assert.IsNotNull(promo);
        }
    }
}

