using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Service.Product;
using Service.Exception;
using Service.Filter.ConcreteFilter;
using Service.User;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using Service.DTO.Product;

namespace Test;
[TestClass]
[ExcludeFromCodeCoverage]
public class ProductServiceTest
{
    public IProductService _productService;
    public  Mock<IProductRepository> _productRepositoryMock;
    public IColorService _colorServiceMock;

    public ProductServiceTest()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _colorServiceMock = new Mock<IColorService>().Object;
        _productService = new ProductService(_productRepositoryMock.Object, _colorServiceMock);
    }

    public static Brand aBrand = new Brand(1)
    {
        Name = "Kova"
    };
    public static Category aCategory = new Category(1)
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
        ProductColors = new List<ProductColor>()

    };
    Color firstColor = new Color(1)
    {
        Name = "Red"
    };
    Color secondColor = new Color(2)
    {
        Name = "Green"
    };
    Color thirdColor = new Color(3)
    {
        Name = "Blue"
    };

    [TestInitialize]
    public void SetUp()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _productService = new ProductService(_productRepositoryMock.Object, _colorServiceMock);

        aBrand = new Brand(3) { Name = "Kova" };
        aCategory = new Category(3) { Name = "Retro" };

        aProduct = new Product()
        {
            Name = "Abdul's Udemy Course",
            Description = "Está godines",
            Price = 10,
            Category = aCategory,
            Brand = aBrand,
            ProductColors = new List<ProductColor>()
        };

        firstColor = new Color(4) { Name = "Red" };
        secondColor = new Color(5) { Name = "Green" };
        thirdColor = new Color(6) { Name = "Blue" };
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
            ProductColors = new List<ProductColor>()
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

        _productRepositoryMock.Verify(repo => repo.Exists(aProduct), Times.Once());
        _productRepositoryMock.Verify(repo => repo.AddProduct(aProduct), Times.Never()); 
    }

    [TestMethod]
    public void DeleteProductWhenProductExistsOk()
    {
        _productRepositoryMock.Setup(repo => repo.Exists(4)).Returns(true);

        _productRepositoryMock.Setup(repo => repo.DeleteProduct(4)); 

        _productService.DeleteProduct(4);
        _productRepositoryMock.Verify(repo => repo.DeleteProduct(4), Times.Once());
    }

    [TestMethod]
    public void DeleteProductWhenProductDoesNotExist()
    {
        _productRepositoryMock.Setup(repo => repo.Exists(4)).Returns(false);

        var exception = Assert.ThrowsException<RepositoryException>(() => _productService.DeleteProduct(4));

        _productRepositoryMock.Verify(repo => repo.Exists(4), Times.Once()); 
        _productRepositoryMock.Verify(repo => repo.DeleteProduct(4), Times.Never()); 
    }

    [TestMethod]
    public void GetProductByName_WhenProductExists_ReturnsProduct()
    {
        _productRepositoryMock.Setup(repo => repo.GetProductByName(aProduct.Name)).Returns(aProduct);

        var result = _productService.GetProductByName(aProduct.Name);

        Assert.AreEqual(aProduct, result);
        _productRepositoryMock.Verify(repo => repo.GetProductByName(aProduct.Name), Times.Once());
    }

    [TestMethod]
    public void GetProductByName_WhenProductDoesNotExist_ThrowsServiceException()
    {
        string nonExistingProductName = "NonExistingProductName";
        _productRepositoryMock.Setup(repo => repo.GetProductByName(nonExistingProductName)).Returns((Product)null);

        var exception = Assert.ThrowsException<RepositoryException>(() => _productService.GetProductByName(nonExistingProductName));
        _productRepositoryMock.Verify(repo => repo.GetProductByName(nonExistingProductName), Times.Once());
    }

    [TestMethod]
    public void GetProductByName_WhenNameIsNull_ThrowsException()
    {
        Assert.ThrowsException<ServiceException>(() => _productService.GetProductByName(null));
    }

    [TestMethod]
    public void GetProductByName_WhenNameIsEmpty_ThrowsException()
    {
        Assert.ThrowsException<ServiceException>(() => _productService.GetProductByName(string.Empty));
    }

    [TestMethod]
    public void GetAllProductsOk()
    {
        var productList = new List<Product>
    {
        new Product { Name = "Product1" },
        new Product { Name = "Product2" }
    };

        _productRepositoryMock.Setup(repo => repo.GetAllProducts(It.IsAny<FilterQuery>())).Returns(productList.ToArray());
        var result = _productService.GetAllProducts(new FilterQuery());
        Assert.AreEqual(productList.Count, result.Length);
        _productRepositoryMock.Verify(repo => repo.GetAllProducts(It.IsAny<FilterQuery>()), Times.Once());
    }

    [TestMethod]
    public void UpdateProduct_UpdatesSuccessfully_WhenProductExists()
    {
        _productRepositoryMock.Setup(repo => repo.Exists(1)).Returns(true);
        _productRepositoryMock.Setup(repo => repo.UpdateProduct(aProduct));

        aProduct.Id = 1;

        _productService.UpdateProduct(aProduct);

        _productRepositoryMock.Verify(repo => repo.Exists(1), Times.Once());
        _productRepositoryMock.Verify(repo => repo.UpdateProduct(aProduct), Times.Once());
    }

    [TestMethod]
    public void UpdateNonExistingProduct()
    {
        _productRepositoryMock.Setup(repo => repo.Exists(aProduct)).Returns(false);

        var exception = Assert.ThrowsException<RepositoryException>(() => _productService.UpdateProduct(aProduct));
        Assert.AreEqual($"Product {aProduct.Id} does not exist.", exception.Message);
        _productRepositoryMock.Verify(repo => repo.Exists(aProduct.Id), Times.Once());
        _productRepositoryMock.Verify(repo => repo.UpdateProduct(aProduct), Times.Never());
    }

    [TestMethod]
    public void UpdateNullProduct()
    {
        Assert.ThrowsException<ServiceException>(() => _productService.UpdateProduct(null));
    }

    [TestMethod]
    public void UpdateProductNameUsingDTO_Ok()
    {
        var updatedProductName = "Updated Product Name";
        var partialDTO = new ProductUpdateModel { Name = updatedProductName };

        _productRepositoryMock.Setup(repo => repo.Exists(1)).Returns(true);
        _productRepositoryMock.Setup(repo => repo.Get(1)).Returns(aProduct);
        _productRepositoryMock.Setup(repo => repo.UpdateProduct(It.IsAny<Product>()));

        _productService.UpdateProductUsingDTO(1, partialDTO);

        _productRepositoryMock.Verify(repo => repo.Get(1), Times.Once());
        _productRepositoryMock.Verify(repo => repo.UpdateProduct(It.Is<Product>(p => p.Name == updatedProductName)), Times.Once());
    }

}

