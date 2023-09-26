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
            List<IProduct> products = new List<IProduct>();

            IPromotionStrategy promo = Logic.GetPromotion(products);
        }

        [TestMethod]
        public void GetPromotion_Ok()
        {
            Mock<IProduct> p1 = PromotionTestHelper.CreateMockProduct();
            Mock<IProduct> p2 = PromotionTestHelper.CreateMockProduct();
            List<IProduct> products = new List<IProduct> { p1.Object, p2.Object };

            IPromotionStrategy promo = Logic.GetPromotion(products);

            Assert.IsNotNull(promo);
        }
    }
}

