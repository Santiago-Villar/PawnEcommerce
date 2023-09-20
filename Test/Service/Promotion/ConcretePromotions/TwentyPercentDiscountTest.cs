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
        var twentyPercentDiscount = new TwentyPercentDiscountTest();
        Assert.IsNotNull(twentyPercentDiscount);
    }
    
    [TestMethod]
    public void ApplyDiscount_TwentyPercentDiscount_Ok()
    {
        var twentyPercentDiscount = new TwentyPercentDiscount();
        
        var mockedProduct = PromotionTestHelper.CreateMockProduct().Object;

        var products = Enumerable.Repeat(mockedProduct, 2).ToList();

        var discountPrice = twentyPercentDiscount.GetDiscountPrice(products);
        const float expectedDiscountPrice = 18;

        Assert.AreEqual(expectedDiscountPrice, discountPrice);
    }
    
    [TestMethod]
    public void GetBestPromotion_TwentyPercentDiscount_Ok()
    {
        var twentyPercentDiscount = new TwentyPercentDiscount();
        
        const string color = "Blue";
        
        var mockColor = PromotionTestHelper.CreateMockColor("Blue").Object;
        var mockColor2 = PromotionTestHelper.CreateMockColor("Red").Object;

        var colors = Enumerable.Repeat(mockColor, 2).ToList(); //error is here
        var colors2 = Enumerable.Repeat(mockColor2, 2).ToList(); //error is here

        var mockCategory1 = PromotionTestHelper.CreateMockCategory("cat1").Object;
        var mockCategory2 = PromotionTestHelper.CreateMockCategory("cat2").Object;

        var mockBrand1 = PromotionTestHelper.CreateMockBrand("brand1").Object;
        var mockBrand2 = PromotionTestHelper.CreateMockBrand("brand2").Object;

        var mockProduct1 = PromotionTestHelper.CreateProduct(mockCategory1, colors, mockBrand1);
        var mockProduct2 = PromotionTestHelper.CreateProduct(mockCategory1, colors, mockBrand1);
        var mockProduct3 = PromotionTestHelper.CreateProduct(mockCategory2, colors2, mockBrand2);

        var products = new List<IProduct>()
        {
            mockProduct1.Object,
            mockProduct2.Object,
            mockProduct3.Object,
        };

        var promotionSelector = new PromotionSelector();
        
        var bestPromotion = promotionSelector.GetBestPromotion(products);

        Assert.AreEqual(twentyPercentDiscount.Name, bestPromotion?.Name);
    }
    
}