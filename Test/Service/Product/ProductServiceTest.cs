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
        Colors = new List<Color>(),
        
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
        _productRepositoryMock = new Mock<IProductRepository>();
        _productService = new ProductService(_productRepositoryMock.Object);

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

        _productRepositoryMock.Setup(repo => repo.AddProduct(aProduct)); 

        _productService.AddProduct(aProduct); 
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

        _productRepositoryMock.Setup(repo => repo.AddProduct(aNewProduct));

        
        _productService.AddProduct(aNewProduct); 
        _productRepositoryMock.Verify(repo => repo.AddProduct(aNewProduct), Times.Once());
        _productRepositoryMock.Verify(repo => repo.AddProduct(aProduct), Times.Never());
    }

    [TestMethod]
    public void AddExistingProduct()
    {

        _productRepositoryMock.Setup(repo => repo.Exists(aProduct)).Returns(true); 

        var exception = Assert.ThrowsException<ServiceException>(() => _productService.AddProduct(aProduct));
        Assert.AreEqual($"Product {aProduct.Name} already exists.", exception.Message);

        _productRepositoryMock.Verify(repo => repo.Exists(aProduct), Times.Once());
        _productRepositoryMock.Verify(repo => repo.AddProduct(aProduct), Times.Never()); 
    }

    [TestMethod]
    public void DeleteProductWhenProductExistsOk()
    {
        _productRepositoryMock.Setup(repo => repo.Exists(aProduct)).Returns(true);

        _productRepositoryMock.Setup(repo => repo.DeleteProduct(aProduct)); 

        _productService.DeleteProduct(aProduct);
        _productRepositoryMock.Verify(repo => repo.DeleteProduct(aProduct), Times.Once());
    }

    [TestMethod]
    public void DeleteProductWhenProductDoesNotExist()
    {
        _productRepositoryMock.Setup(repo => repo.Exists(aProduct)).Returns(false);

        var exception = Assert.ThrowsException<ServiceException>(() => _productService.DeleteProduct(aProduct));
        Assert.AreEqual("Product " + aProduct.Name + " does not exist.", exception.Message);

        _productRepositoryMock.Verify(repo => repo.Exists(aProduct), Times.Once()); 
        _productRepositoryMock.Verify(repo => repo.DeleteProduct(aProduct), Times.Never()); 
    }

    [TestMethod]
    public void GetProductByName_WhenProductExists_ReturnsProduct()
    {
        // Arrange
        _productRepositoryMock.Setup(repo => repo.GetProductByName(aProduct.Name)).Returns(aProduct);

        // Act
        var result = _productService.GetProductByName(aProduct.Name);

        // Assert
        Assert.AreEqual(aProduct, result);
        _productRepositoryMock.Verify(repo => repo.GetProductByName(aProduct.Name), Times.Once());
    }

    [TestMethod]
    public void GetProductByName_WhenProductDoesNotExist_ThrowsServiceException()
    {
        string nonExistingProductName = "NonExistingProductName";
        _productRepositoryMock.Setup(repo => repo.GetProductByName(nonExistingProductName)).Returns((Product)null);

        var exception = Assert.ThrowsException<ServiceException>(() => _productService.GetProductByName(nonExistingProductName));
        Assert.AreEqual($"Product {nonExistingProductName} does not exist.", exception.Message);
        _productRepositoryMock.Verify(repo => repo.GetProductByName(nonExistingProductName), Times.Once());
    }

    [TestMethod]
    public void GetProductByName_WhenNameIsNull_ThrowsException()
    {
        Assert.ThrowsException<ArgumentException>(() => _productService.GetProductByName(null));
    }

    [TestMethod]
    public void GetProductByName_WhenNameIsEmpty_ThrowsException()
    {
        Assert.ThrowsException<ArgumentException>(() => _productService.GetProductByName(string.Empty));
    }

    [TestMethod]
    public void GetAllProductsOk()
    {
        var productList = new List<Product>
    {
        new Product { Name = "Product1" },
        new Product { Name = "Product2" }
    };

        _productRepositoryMock.Setup(repo => repo.GetAllProducts()).Returns(productList.ToArray());
        var result = _productService.GetAllProducts();
        Assert.AreEqual(productList.Count, result.Length);
        _productRepositoryMock.Verify(repo => repo.GetAllProducts(), Times.Once());
    }

    [TestMethod]
    public void UpdateProduct_UpdatesSuccessfully_WhenProductExists()
    {
        _productRepositoryMock.Setup(repo => repo.Exists(aProduct)).Returns(true);
        _productRepositoryMock.Setup(repo => repo.UpdateProduct(aProduct));

        _productService.UpdateProduct(aProduct);

        _productRepositoryMock.Verify(repo => repo.Exists(aProduct), Times.Once());
        _productRepositoryMock.Verify(repo => repo.UpdateProduct(aProduct), Times.Once());
    }

    [TestMethod]
    public void UpdateNonExistingProduct()
    {
        _productRepositoryMock.Setup(repo => repo.Exists(aProduct)).Returns(false);

     
        var exception = Assert.ThrowsException<ServiceException>(() => _productService.UpdateProduct(aProduct));
        Assert.AreEqual($"Product {aProduct.Name} does not exist.", exception.Message);
        _productRepositoryMock.Verify(repo => repo.Exists(aProduct), Times.Once());
        _productRepositoryMock.Verify(repo => repo.UpdateProduct(aProduct), Times.Never());
    }



}

