using Service.Product;
using Service.Promotion.ConcreteStrategies;
using Service.Promotion;
using Test.Service.Promotion;

namespace Test.Service.Promotion.ConcretePromotions;

[TestClass]
public class TotalLookDiscountTest
{
    [TestMethod]
    public void CanCreateTotalLook_Ok()
    {
        var totalLook = new TotalLook();
        Assert.IsNotNull(totalLook);
    }

    [TestMethod]
    public void ApplyDiscount_TotalLook_Ok()
    {
        var totalLook = new TotalLook();

        var product = PromotionTestHelper.CreateProduct();
        var products = Enumerable.Repeat(product, 4).ToList();

        var discountPrice = totalLook.GetDiscountPrice(products);
        const float expectedDiscountPrice = 35;

        Assert.AreEqual(expectedDiscountPrice, discountPrice);
    }

    [TestMethod]
    public void GetBestPromotion_TotalLook_Ok()
    {
        var totalLook = new TotalLook();

        var color = PromotionTestHelper.CreateColor("Blue");
        var colors = new List<Color> { color, color };

        var category1 = PromotionTestHelper.CreateCategory("cat1");
        var category2 = PromotionTestHelper.CreateCategory("cat2");

        var brand1 = PromotionTestHelper.CreateBrand("brand1");
        var brand2 = PromotionTestHelper.CreateBrand("brand2");

        var product1 = PromotionTestHelper.CreateProduct(category1, colors, brand1);
        var product2 = PromotionTestHelper.CreateProduct(category1, colors, brand1);
        var product3 = PromotionTestHelper.CreateProduct(category2, colors, brand2);

        var products = new List<Product> { product1, product2, product3 };

        var promotionSelector = new PromotionSelector();

        var bestPromotion = promotionSelector.GetBestPromotion(products);

        Assert.AreEqual(totalLook.Name, bestPromotion?.Name);
    }
}


