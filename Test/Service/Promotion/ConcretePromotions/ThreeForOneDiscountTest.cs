using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service.Promotion;
using Service.Promotion.ConcreteStrategies;
using System.Linq;

namespace Test.Service.Promotion.ConcretePromotions
{
    [TestClass]
    public class ThreeForOneDiscountTest
    {
        [TestMethod]
        public void CanCreateThreeForOne_Ok()
        {
            var threeForOne = new ThreeForOne();
            Assert.IsNotNull(threeForOne);
        }

        [TestMethod]
        public void ApplyDiscount_ThreeForOne_Ok()
        {
            var threeForOne = new ThreeForOne();

            var product = PromotionTestHelper.CreateProduct();

            var products = Enumerable.Repeat(product, 4).ToList();

            var discountPrice = threeForOne.GetDiscountPrice(products);
            const float expectedDiscountPrice = 20;

            Assert.AreEqual(expectedDiscountPrice, discountPrice);
        }

        [TestMethod]
        public void GetBestPromotion_ThreeForOne_Ok()
        {
            var threeForOne = new ThreeForOne();
            var product = PromotionTestHelper.CreateProduct();

            var products = Enumerable.Repeat(product, 5).ToList();

            var promotionSelector = new PromotionSelector();

            var bestPromotion = promotionSelector.GetBestPromotion(products);

            Assert.AreEqual(threeForOne.Name, bestPromotion?.Name);
        }
    }
}
