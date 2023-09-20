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
    private Mock<ICategory> CreateMockCategory(string name)
    {
        var mockCategory = new Mock<ICategory>();
        mockCategory.Setup(cat => cat.Name).Returns(name);
        return mockCategory;
    }
    
    private Mock<IBrand> CreateMockBrand(string name)
    {
        var mockBrand = new Mock<IBrand>();
        mockBrand.Setup(bra => bra.Name).Returns(name);
        return mockBrand;
    }
    
    private Mock<IColor> CreateMockColor(string name)
    {
        var mockColor = new Mock<IColor>();
        mockColor.Setup(col => col.Name).Returns(name);
        return mockColor;
    }

    private Mock<IProduct> CreateMockProduct()
    {
        const int price = 10;
        const string category = "Jeans";
        const string color = "Blue";
        const string brand = "Zara";

        var mockColor = CreateMockColor(color).Object;
        var colors = Enumerable.Repeat(mockColor, 3).ToList();

        var mockCategory = CreateMockCategory(category).Object;
        var mockBrand = CreateMockBrand(brand).Object;
        
        var mockProduct = new Mock<IProduct>();
        mockProduct.Setup(product => product.Category).Returns(mockCategory);
        mockProduct.Setup(product => product.Price).Returns(price);
        mockProduct.Setup(product => product.Colors).Returns(colors);
        mockProduct.Setup(product => product.Brand).Returns(mockBrand);

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
        const float expectedDiscountPrice = 35;

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
        
        var promotionsName = new List<string>()
        {
            "Total Look",
             "Three For One",
             "Three For Two",
             "Twenty Percent Discount"
        };

        var promotions = promotionsCollection.GetPromotions();

        var names = promotions.Select(promotion => promotion.Name).ToList();
        
        CollectionAssert.AreEquivalent(promotionsName, names);
    }

    [TestMethod]
    public void GetBestPromotion_Ok()
    {
        var threeForOne = new ThreeForOne();
        var mockedProduct = CreateMockProduct().Object;

        var products = Enumerable.Repeat(mockedProduct, 5).ToList();

        var promotionSelector = new PromotionSelector();

        var bestPromotion = promotionSelector.GetBestPromotion(products);

        Assert.AreEqual(threeForOne.Name, bestPromotion?.Name);
    }

    [TestMethod]
    public void GetBestPromotion_TotalLook_Ok()
    {
        var totalLook = new TotalLook();
        
        const string color = "Blue";
        
        var mockColor = CreateMockColor("Blue").Object;

        var colors = Enumerable.Repeat(mockColor, 2).ToList(); //error is here

        var mockCategory1 = CreateMockCategory("cat1").Object;
        var mockCategory2 = CreateMockCategory("cat2").Object;

        var mockBrand1 = CreateMockBrand("brand1").Object;
        var mockBrand2 = CreateMockBrand("brand2").Object;

        var mockProduct1 = CreateProduct(mockCategory1, colors, mockBrand1);
        var mockProduct2 = CreateProduct(mockCategory1, colors, mockBrand1);
        var mockProduct3 = CreateProduct(mockCategory2, colors, mockBrand2);

        var products = new List<IProduct>()
        {
            mockProduct1.Object,
            mockProduct2.Object,
            mockProduct3.Object,
        };

        var promotionSelector = new PromotionSelector();
        
        var bestPromotion = promotionSelector.GetBestPromotion(products);

        Assert.AreEqual(totalLook.Name, bestPromotion?.Name);
        return;

        Mock<IProduct> CreateProduct(ICategory category, List<IColor> list, IBrand brand)
        {
            const int price = 10;

            var mock = new Mock<IProduct>();
            mock.Setup(product => product.Category).Returns(category);
            mock.Setup(product => product.Price).Returns(price);
            mock.Setup(product => product.Colors).Returns(list);
            mock.Setup(product => product.Brand).Returns(brand);
            
            return mock;
        }
    }
    
    [TestMethod]
    public void GetBestPromotion_ThreeForTwo_Ok()
    {
        var threeForTwo = new ThreeForTwo();
        
        const string color = "Blue";
        
        var mockColor = CreateMockColor("Blue").Object;

        var colors = Enumerable.Repeat(mockColor, 2).ToList(); //error is here

        var mockCategory1 = CreateMockCategory("cat1").Object;
        var mockCategory2 = CreateMockCategory("cat2").Object;

        var mockBrand1 = CreateMockBrand("brand1").Object;
        var mockBrand2 = CreateMockBrand("brand2").Object;

        var mockProduct1 = CreateProduct(mockCategory1, colors, mockBrand1);
        var mockProduct2 = CreateProduct(mockCategory1, colors, mockBrand1);
        var mockProduct3 = CreateProduct(mockCategory1, colors, mockBrand2);

        var products = new List<IProduct>()
        {
            mockProduct1.Object,
            mockProduct2.Object,
            mockProduct3.Object,
        };

        var promotionSelector = new PromotionSelector();
        
        var bestPromotion = promotionSelector.GetBestPromotion(products);

        Assert.AreEqual(threeForTwo.Name, bestPromotion?.Name);
        return;

        Mock<IProduct> CreateProduct(ICategory category, List<IColor> list, IBrand brand)
        {
            const int price = 10;

            var mock = new Mock<IProduct>();
            mock.Setup(product => product.Category).Returns(category);
            mock.Setup(product => product.Price).Returns(price);
            mock.Setup(product => product.Colors).Returns(list);
            mock.Setup(product => product.Brand).Returns(brand);
            
            return mock;
        }
    }
}