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
        // Aquí solo verificamos que se llama a AddProduct sin excepciones
        _productRepositoryMock.Setup(repo => repo.AddProduct(aProduct)); // Configuración del mock sin retorno

        // Act & Assert
        _productService.AddProduct(aProduct); // Si no lanza excepciones, el test pasará
        _productRepositoryMock.Verify(repo => repo.AddProduct(aProduct), Times.Once());
    }

    [TestMethod]
    public void AddProductFails()
    {
        Product aNewProduct = new Product()
        {
            Name = "Camiseta del glorioso Real Betis Balompié",
            Description = "OLE OLE OLE OLE BETIOLÉÉÉ",
            Price = 1000,
            Category = aCategory,
            Brand = aBrand,
            Colors = new List<Color>()
        };
        // Aquí solo verificamos que se llama a AddProduct sin excepciones
        _productRepositoryMock.Setup(repo => repo.AddProduct(aNewProduct)); // Configuración del mock sin retorno

        // Act & Assert
        _productService.AddProduct(aNewProduct); // Si no lanza excepciones, el test pasará
        _productRepositoryMock.Verify(repo => repo.AddProduct(aNewProduct), Times.Once());
        _productRepositoryMock.Verify(repo => repo.AddProduct(aProduct), Times.Never());
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
    public void DeleteProductWhenProductExistsOk()
    {
        _productRepositoryMock.Setup(repo => repo.Exists(aProduct)).Returns(true);
        // Aquí solo verificamos que se llama a DeleteProduct sin excepciones
        _productRepositoryMock.Setup(repo => repo.DeleteProduct(aProduct)); // Configuración del mock sin retorno

        // Act & Assert
        _productService.DeleteProduct(aProduct); // Si no lanza excepciones, el test pasará
        _productRepositoryMock.Verify(repo => repo.DeleteProduct(aProduct), Times.Once());
    }

    [TestMethod]
    public void DeleteProductWhenProductDoesNotExist()
    {
        // Arrange: configurar el mock para que indique que el producto NO existe
        _productRepositoryMock.Setup(repo => repo.Exists(aProduct)).Returns(false);

        // Act & Assert: esperamos una excepción al intentar borrar el producto
        var exception = Assert.ThrowsException<ServiceException>(() => _productService.DeleteProduct(aProduct));
        Assert.AreEqual("Product " + aProduct.Name + " does not exist.", exception.Message);

        _productRepositoryMock.Verify(repo => repo.Exists(aProduct), Times.Once()); // Verificamos que se comprobó la existencia del producto
        _productRepositoryMock.Verify(repo => repo.DeleteProduct(aProduct), Times.Never()); // Verificamos que nunca se intentó borrar el producto
    }

    [TestMethod]
    public void GetProductByName_WhenProductExists_ReturnsProduct()
    {
        // Arrange
        _productRepositoryMock.Setup(repo => repo.GetProductByName(aProduct.Name, It.IsAny<IUser>())).Returns(aProduct);

        // Act
        var result = _productService.GetProductByName(aProduct.Name, It.IsAny<IUser>());

        // Assert
        Assert.AreEqual(aProduct, result);
        _productRepositoryMock.Verify(repo => repo.GetProductByName(aProduct.Name, It.IsAny<IUser>()), Times.Once());
    }

    [TestMethod]
    public void GetProductByName_WhenProductDoesNotExist_ThrowsServiceException()
    {
        // Arrange
        string nonExistingProductName = "NonExistingProductName";
        _productRepositoryMock.Setup(repo => repo.GetProductByName(nonExistingProductName, It.IsAny<IUser>())).Returns((Product)null);

        // Act & Assert
        var exception = Assert.ThrowsException<ServiceException>(() => _productService.GetProductByName(nonExistingProductName, It.IsAny<IUser>()));
        Assert.AreEqual($"Product {nonExistingProductName} does not exist.", exception.Message);
        _productRepositoryMock.Verify(repo => repo.GetProductByName(nonExistingProductName, It.IsAny<IUser>()), Times.Once());
    }



}

