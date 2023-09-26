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

		public PromotionLogicTest()
		{
		}

        [TestMethod]
        public void CanCreatePromotionsLogic_Ok()
        {
            IPromotionLogic logic = new PromotionLogic();
            Assert.IsNotNull(logic);
        }

        [ExpectedException(typeof(ServiceException))]
        [TestMethod]
        public void GetPromotionWithNoProductsThrowsException()
        {
            IPromotionLogic logic = new PromotionLogic();
            List<IProduct> products = new List<IProduct>();
            IPromotionStrategy promo = logic.GetPromotion(products);
        }

        [TestMethod]
        public void GetPromotion_Ok()
        {
            IPromotionLogic logic = new PromotionLogic();

            Mock<IProduct> p1 = PromotionTestHelper.CreateMockProduct();
            Mock<IProduct> p2 = PromotionTestHelper.CreateMockProduct();
            List<IProduct> products = new List<IProduct> { p1.Object, p2.Object };

            IPromotionStrategy promo = logic.GetPromotion(products);
            Assert.IsNotNull(promo);
        }
    }
}

