using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service.Product;
using Service.Promotion;
using Service.Promotion.ConcreteStrategies;
using System.Collections.Generic;
using System.Linq;

namespace Test.Service.Promotion.ConcretePromotions
{
    [TestClass]
    public class ThreeForTwoTest
    {
        [TestMethod]
        public void CanCreateThreeForTwo_Ok()
        {
            var threeForTwo = new ThreeForTwo();
            Assert.IsNotNull(threeForTwo);
        }

        [TestMethod]
        public void ApplyDiscount_ThreeForTwo_Ok()
        {
            var threeForTwoDiscount = new ThreeForTwo();

            var product = PromotionTestHelper.CreateProduct();
            var products = Enumerable.Repeat(product, 4).ToList();

            var discountPrice = threeForTwoDiscount.GetDiscountPrice(products);
            const float expectedDiscountPrice = 30;

            Assert.AreEqual(expectedDiscountPrice, discountPrice);
        }

        [TestMethod]
        public void GetBestPromotion_ThreeForTwo_Ok()
        {
            var threeForTwo = new ThreeForTwo();

            var color = PromotionTestHelper.CreateColor("Blue");
            var colors = Enumerable.Repeat(color, 2).ToList();

            var category1 = PromotionTestHelper.CreateCategory("cat1");
            var category2 = PromotionTestHelper.CreateCategory("cat2");

            var brand1 = PromotionTestHelper.CreateBrand("brand1");
            var brand2 = PromotionTestHelper.CreateBrand("brand2");

            var product1 = PromotionTestHelper.CreateProduct(category1, colors, brand1);
            var product2 = PromotionTestHelper.CreateProduct(category1, colors, brand1);
            var product3 = PromotionTestHelper.CreateProduct(category1, colors, brand2);

            var products = new List<Product>
            {
                product1,
                product2,
                product3
            };

            var promotionSelector = new PromotionSelector();

            var bestPromotion = promotionSelector.GetBestPromotion(products);

            Assert.AreEqual(threeForTwo.Name, bestPromotion?.Name);
        }
    }
}
