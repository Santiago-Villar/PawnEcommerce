using System.Drawing;
using Service.Promotion.ConcreteStrategies;
using Moq;
using Service.Product;
using Service.Product.Brand;
using Service.Product.Category;
using Service.Product.Color;
using Service.Promotion;

namespace Test.Service.Promotion;

[TestClass]
public class PromotionTest
{
    private Mock<IProduct> CreateMockProduct()
    {
        const int price = 10;
        const string category = "Jeans";
        const string color = "Blue";
        const string brand = "Zara";

        var mockColor = new Mock<IColor>();
        mockColor.Setup(col => col.Name).Returns(color);
        var mockedColor = mockColor.Object;
        
        var colors = Enumerable.Repeat(mockedColor, 3).ToList();
        
        var mockCategory = new Mock<ICategory>();
        mockCategory.Setup(cat => cat.Name).Returns(category);
        var mockedCategory = mockCategory.Object;
        
        var mockBrand = new Mock<IBrand>();
        mockBrand.Setup(bra => bra.Name).Returns(brand);
        var mockedBrand = mockBrand.Object;
        
        var mockProduct = new Mock<IProduct>();
        mockProduct.Setup(product => product.Category).Returns(mockedCategory);
        mockProduct.Setup(product => product.Price).Returns(price);
        mockProduct.Setup(product => product.Colors).Returns(colors);
        mockProduct.Setup(product => product.Brand).Returns(mockedBrand);

        return mockProduct;
    }
    
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
        
        var mockedProduct = CreateMockProduct().Object;

        var products = Enumerable.Repeat(mockedProduct, 2).ToList();

        var discountPrice = twentyPercentDiscount.GetDiscountPrice(products);
        const float expectedDiscountPrice = 18;

        Assert.AreEqual(expectedDiscountPrice, discountPrice);
    }
    
    [TestMethod]
    public void ApplyDiscount_ThreeForTwo_Ok()
    {
        var threeForTwoDiscount = new ThreeForTwo();

        var mockedProduct = CreateMockProduct().Object;

        var products = Enumerable.Repeat(mockedProduct, 4).ToList();
        
        var discountPrice = threeForTwoDiscount.GetDiscountPrice(products);
        const float expectedDiscountPrice = 30;

        Assert.AreEqual(expectedDiscountPrice, discountPrice);
    }
    
    [TestMethod]
    public void ApplyDiscount_TotalLook_Ok()
    {
        var totalLook = new TotalLook();

        var mockedProduct = CreateMockProduct().Object;

        var products = Enumerable.Repeat(mockedProduct, 4).ToList();
        
        var discountPrice = totalLook.GetDiscountPrice(products);
        const float expectedDiscountPrice = 30;

        Assert.AreEqual(expectedDiscountPrice, discountPrice);
    }
    
    [TestMethod]
    public void ApplyDiscount_ThreeForOne_Ok()
    {
        var threeForOne = new ThreeForOne();

        var mockedProduct = CreateMockProduct().Object;

        var products = Enumerable.Repeat(mockedProduct, 4).ToList();
        
        var discountPrice = threeForOne.GetDiscountPrice(products);
        const float expectedDiscountPrice = 20;

        Assert.AreEqual(expectedDiscountPrice, discountPrice);
    }
    
    [TestMethod]
    public void CanCreatePromotionsCollection_Ok()
    {
        var promotionsCollection = new PromotionCollection();
        Assert.IsNotNull(promotionsCollection);
    }
    
    [TestMethod]
    public void GetPromotions_Ok()
    {
        var promotionsCollection = new PromotionCollection();
        
        var promotions = new List<IPromotionStrategy>()
        {
            new TotalLook(),
            new ThreeForOne(),
            new ThreeForTwo(),
            new TwentyPercentDiscount()
        };
        
        CollectionAssert.IsSubsetOf(promotions, promotionsCollection.GetPromotions());
    }
    
}