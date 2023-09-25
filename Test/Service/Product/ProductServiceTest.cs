using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Service.Product;
using Service.User;

namespace Test;
[TestClass]
public class ProductServiceTest
{
    public IProductService _productService;
   public  Mock<IProductRepository> _productRepositoryMock;

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

     Product aProduct = new Product()
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

    [TestInitialize]
    public void SetUp()
    {
        // Recrear mocks
        _productRepositoryMock = new Mock<IProductRepository>();
        _productService = new ProductService(_productRepositoryMock.Object);

        // Recrear instancias de objetos
        aBrand = new Brand() { Name = "Kova" };
        aCategory = new Category() { Name = "Retro" };

        aProduct = new Product()
        {
            Name = "Abdul's Udemy Course",
            Description = "Está godines",
            Price = 10,
            Category = aCategory,
            Brand = aBrand,
            Colors = new List<Color>()
        };

        firstColor = new Color() { Name = "Red" };
        secondColor = new Color() { Name = "Green" };
        thirdColor = new Color() { Name = "Blue" };
    }


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

    [TestMethod]
    public void AddProductFails()
    {

        Product differentProduct = new Product()
        {
            Name = "Camiseta del Real Betis Balompié",
            Description = "OLE OLE OLE OLE BETIS OLÉ",
            Price = 10,
            Category = aCategory,
            Brand = aBrand,
            Colors = new List<Color>()
        };
        _productRepositoryMock.Setup(repo => repo.AddProduct(differentProduct)).Returns(differentProduct);

        // Act
        var result = _productService.AddProduct(differentProduct);

        // Assert
        Assert.AreNotEqual(aProduct, result);
        _productRepositoryMock.Verify(repo => repo.AddProduct(differentProduct), Times.Once());
    }


    [TestMethod]
    public void AddExistingProduct()
    {
        // Arrange
        _productRepositoryMock.Setup(repo => repo.Exists(aProduct)).Returns(true); // Indicamos que el producto ya existe

        // Act & Assert
        var exception = Assert.ThrowsException<ServiceException>(() => _productService.AddProduct(aProduct));
        Assert.AreEqual($"Product {aProduct.Name} already exists.", exception.Message);

        _productRepositoryMock.Verify(repo => repo.Exists(aProduct), Times.Once());
        _productRepositoryMock.Verify(repo => repo.AddProduct(aProduct), Times.Never()); // Verificamos que nunca se intentó agregar el producto
    }

    [TestMethod]
    public void DeleteProduct_ShouldDeleteAndReturnProduct_WhenProductExists()
    {
        // Arrange
        _productRepositoryMock.Setup(repo => repo.Exists(aProduct)).Returns(true);
        _productRepositoryMock.Setup(repo => repo.DeleteProduct(aProduct)).Returns(aProduct);

        // Act
        var result = _productService.DeleteProduct(aProduct);

        // Assert
        Assert.AreEqual(aProduct, result);
        _productRepositoryMock.Verify(repo => repo.Exists(aProduct), Times.Once());
        _productRepositoryMock.Verify(repo => repo.DeleteProduct(aProduct), Times.Once());
    }


}

