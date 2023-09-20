using Moq;
using Service.Product;

namespace Test.Service.Promotion;

public class PromotionTestHelper
{
    internal static Mock<IProduct> CreateMockProduct()
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
    
    internal static Mock<IProduct> CreateProduct(ICategory category, List<IColor> list, IBrand brand)
    {
        const int price = 10;

        var mock = new Mock<IProduct>();
        mock.Setup(product => product.Category).Returns(category);
        mock.Setup(product => product.Price).Returns(price);
        mock.Setup(product => product.Colors).Returns(list);
        mock.Setup(product => product.Brand).Returns(brand);
            
        return mock;
    }
    
    internal static Mock<ICategory> CreateMockCategory(string name)
    {
        var mockCategory = new Mock<ICategory>();
        mockCategory.Setup(cat => cat.Name).Returns(name);
        return mockCategory;
    }
    
    internal static Mock<IBrand> CreateMockBrand(string name)
    {
        var mockBrand = new Mock<IBrand>();
        mockBrand.Setup(bra => bra.Name).Returns(name);
        return mockBrand;
    }
    
    internal static Mock<IColor> CreateMockColor(string name)
    {
        var mockColor = new Mock<IColor>();
        mockColor.Setup(col => col.Name).Returns(name);
        return mockColor;
    }
}