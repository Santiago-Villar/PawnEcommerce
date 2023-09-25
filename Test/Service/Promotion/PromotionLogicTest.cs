using System;
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
    }
}

