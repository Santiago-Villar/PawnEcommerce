using Service.Product;
using Service.Promotion;
using Service.Promotion.ConcreteStrategies;

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

        var mockedProduct = PromotionTestHelper.CreateMockProduct().Object;

        var products = Enumerable.Repeat(mockedProduct, 4).ToList();
        
        var discountPrice = totalLook.GetDiscountPrice(products);
        const float expectedDiscountPrice = 35;

        Assert.AreEqual(expectedDiscountPrice, discountPrice);
    }
    
    [TestMethod]
    public void GetBestPromotion_TotalLook_Ok()
    {
        var totalLook = new TotalLook();
        
        const string color = "Blue";
        
        var mockColor = PromotionTestHelper.CreateMockColor("Blue").Object;

        var colors = Enumerable.Repeat(mockColor, 2).ToList(); //error is here

        var mockCategory1 = PromotionTestHelper.CreateMockCategory("cat1").Object;
        var mockCategory2 = PromotionTestHelper.CreateMockCategory("cat2").Object;

        var mockBrand1 = PromotionTestHelper.CreateMockBrand("brand1").Object;
        var mockBrand2 = PromotionTestHelper.CreateMockBrand("brand2").Object;

        var mockProduct1 = PromotionTestHelper.CreateProduct(mockCategory1, colors, mockBrand1);
        var mockProduct2 = PromotionTestHelper.CreateProduct(mockCategory1, colors, mockBrand1);
        var mockProduct3 = PromotionTestHelper.CreateProduct(mockCategory2, colors, mockBrand2);

        var products = new List<IProduct>()
        {
            mockProduct1.Object,
            mockProduct2.Object,
            mockProduct3.Object,
        };

        var promotionSelector = new PromotionSelector();
        
        var bestPromotion = promotionSelector.GetBestPromotion(products);

        Assert.AreEqual(totalLook.Name, bestPromotion?.Name);
    }
}