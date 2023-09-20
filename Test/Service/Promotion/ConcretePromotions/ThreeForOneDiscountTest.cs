using Service.Promotion;
using Service.Promotion.ConcreteStrategies;

namespace Test.Service.Promotion.ConcretePromotions;

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

        var mockedProduct = PromotionTestHelper.CreateMockProduct().Object;

        var products = Enumerable.Repeat(mockedProduct, 4).ToList();
        
        var discountPrice = threeForOne.GetDiscountPrice(products);
        const float expectedDiscountPrice = 20;

        Assert.AreEqual(expectedDiscountPrice, discountPrice);
    }
    
    [TestMethod]
    public void GetBestPromotion_ThreeForOne_Ok()
    {
        var threeForOne = new ThreeForOne();
        var mockedProduct = PromotionTestHelper.CreateMockProduct().Object;

        var products = Enumerable.Repeat(mockedProduct, 5).ToList();

        var promotionSelector = new PromotionSelector();

        var bestPromotion = promotionSelector.GetBestPromotion(products);

        Assert.AreEqual(threeForOne.Name, bestPromotion?.Name);
    }
    
}