using System;
using Service.Exception;
using Service.Product;
using Service.Promotion;

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
    }
}

