using Service.Promotion.ConcreteStrategies;
using Moq;
using Service.Product;

namespace Test.Promotion;

[TestClass]
public class PromotionTest
{
    [TestMethod]
    public void CanCreateTotalLook_Ok()
    {
        var totalLook = new TotalLook();
        Assert.IsNotNull(totalLook);
    }
    
    [TestMethod]
    public void CanCreateThreeForOne_Ok()
    {
        var threeForOne = new ThreeForOne();
        Assert.IsNotNull(threeForOne);
    }
    
    [TestMethod]
    public void CanCreateThreeForTwo_Ok()
    {
        var threeForTwo = new ThreeForTwo();
        Assert.IsNotNull(threeForTwo);
    }
    
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
        const int productPrice = 10;
        
        var mockProduct = new Mock<IProduct>();
        mockProduct.Setup(product => product.Price).Returns(productPrice);
        var mockedProduct = mockProduct.Object;
        
        var products = new List<IProduct>()
        {
            mockedProduct,
            mockedProduct
        };
        
        var discountPrice = twentyPercentDiscount.GetDiscountPrice(products);
        const float expectedDiscountPrice = 18;

        Assert.AreEqual(expectedDiscountPrice, discountPrice);
    }}