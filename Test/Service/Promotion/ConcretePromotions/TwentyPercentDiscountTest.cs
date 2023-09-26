using Service.Product;
using Service.Promotion;
using Service.Promotion.ConcreteStrategies;

namespace Test.Service.Promotion.ConcretePromotions;

[TestClass]
public class TwentyPercentDiscountTest
{
    [TestMethod]
    public void TwentyPercentDiscount_Ok()
    {
        var twentyPercentDiscount = new TwentyPercentDiscount();
        Assert.IsNotNull(twentyPercentDiscount);
    }

    [TestMethod]
    public void ApplyDiscount_TwentyPercentDiscount_Ok()
    {
        var twentyPercentDiscount = new TwentyPercentDiscount();

        var product = PromotionTestHelper.CreateProduct();
        var products = Enumerable.Repeat(product, 2).ToList();

        var discountPrice = twentyPercentDiscount.GetDiscountPrice(products);
        const float expectedDiscountPrice = 18;

        Assert.AreEqual(expectedDiscountPrice, discountPrice);
    }

    [TestMethod]
    public void GetBestPromotion_TwentyPercentDiscount_Ok()
    {
        var twentyPercentDiscount = new TwentyPercentDiscount();

        var color1 = PromotionTestHelper.CreateColor("Blue");
        var color2 = PromotionTestHelper.CreateColor("Red");

        var colors = new List<Color> { color1, color1 };
        var colors2 = new List<Color> { color2, color2 };

        var category1 = PromotionTestHelper.CreateCategory("cat1");
        var category2 = PromotionTestHelper.CreateCategory("cat2");

        var brand1 = PromotionTestHelper.CreateBrand("brand1");
        var brand2 = PromotionTestHelper.CreateBrand("brand2");

        var product1 = PromotionTestHelper.CreateProduct(category1, colors, brand1);
        var product2 = PromotionTestHelper.CreateProduct(category1, colors, brand1);
        var product3 = PromotionTestHelper.CreateProduct(category2, colors2, brand2);

        var products = new List<Product> { product1, product2, product3 };

        var promotionSelector = new PromotionSelector();

        var bestPromotion = promotionSelector.GetBestPromotion(products);

        Assert.AreEqual(twentyPercentDiscount.Name, bestPromotion?.Name);
    }
}

    
