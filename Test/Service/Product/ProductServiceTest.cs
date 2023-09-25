using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic;
using IRepository;
using Service.Product;
using Service.User;

[TestClass]
public class ProductServiceTest
{
    private readonly IProductService _productService;
    private readonly Mock<IProductRepository> _productRepositoryMock;

    public ProductServiceTest()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _productService = new ProductService(_productRepositoryMock.Object);
    }

    public static Brand aBrand = new Brand()
    {
        Name = "Kova"
    };
    public static Category aCategory = new Category()
    {
        Name = "Retro"
    };

    public static Product aProduct = new Product()
    {
        Name = "Abdul's Udemy Course",
        Description = "Está godines",
        Price = 10,
        Category = aCategory,
        Brand = aBrand,
        Colors = new List<Color>()
    };
    Color firstColor = new Color()
    {
        Name = "Red"
    };
    Color secondColor = new Color()
    {
        Name = "Green"
    };
    Color thirdColor = new Color()
    {
        Name = "Blue"
    };


    [TestMethod]
    public void AddProductOk()
    {

        _productRepositoryMock.Setup(repo => repo.AddProduct(aProduct)).Returns(aProduct);

        // Act
        var result = _productService.AddProduct(aProduct);

        // Assert
        Assert.AreEqual(aProduct, result);
        _productRepositoryMock.Verify(repo => repo.AddProduct(aProduct), Times.Once());
    }
}

