using Microsoft.AspNetCore.Mvc;
using Moq;
using PawnEcommerce.Controllers;
using Service.DTO.Product;
using Service.DTO.Sale;
using Service.Product;
using Service.Promotion;
using Service.Promotion.ConcreteStrategies;
using Service.Sale;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace Test.Controller;

[TestClass]
[ExcludeFromCodeCoverage]
public class SaleControllerTest
{
    private static readonly ProductDTO ProductDto = new ProductDTO()
    {
        Id = 1,
        Name = "testProd",
        Description = "test description",
        Price = 10,
        Brand = new BrandDTO() {Name = "none", Id = 1},
        Category = new CategoryDTO() {Name = "none", Id = 1},
        Colors = new ColorDTO[] 
        { 
            new ColorDTO(){Name = "blue", Id = 1},
            new ColorDTO(){Name = "red", Id = 2}
        }
    };

    private readonly SaleCreationModel _newSale = new SaleCreationModel()
    {
        ProductDtosId = new int[]
        {
            1
        },
    };
    
    [TestMethod]
    public void CanCreateController_Ok()
    {
        var saleService = new Mock<ISaleService>();
        var productService = new Mock<IProductService>();

        var saleController = new SaleController(saleService.Object, productService.Object);
        Assert.IsNotNull(saleController);
    }
    
    [TestMethod]
    public void Create_Ok()
    {
        var saleService = new Mock<ISaleService>();
        var productService = new Mock<IProductService>();
        productService.Setup(ps => ps.Get(It.IsAny<int>())).Returns(new Product());

        var saleController = new SaleController(saleService.Object, productService.Object);

        var result = saleController.Create(_newSale) as OkResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);
    }
    
    [TestMethod]
    public void GetAll_Ok()
    {
        var sales = Enumerable.Repeat(_newSale, 3).Select(sale => sale.ToEntity()).ToList();

        var saleService = new Mock<ISaleService>();
        saleService.Setup(ps => ps.GetAll()).Returns(sales);
        var productService = new Mock<IProductService>();

        var saleController = new SaleController(saleService.Object, productService.Object);
        var result = saleController.GetAll() as OkObjectResult;

        Assert.IsNotNull(result);
        CollectionAssert.AreEqual(sales, result.Value as List<Sale>);
    }
    
    [TestMethod]
    public void GetById_Ok()
    {
        var sales = Enumerable.Repeat(_newSale, 3).Select(sale => sale.ToEntity()).ToList();
        var foundSaleWithId = sales[0];

        var saleService = new Mock<ISaleService>();
        saleService.Setup(ps => ps.Get(1)).Returns(foundSaleWithId);
        var productService = new Mock<IProductService>();

        var saleController = new SaleController(saleService.Object, productService.Object);
        var result = saleController.Get(1) as OkObjectResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(foundSaleWithId, result.Value);
    }
    
    [TestMethod]
    public void GetDiscount_Ok()
    {
        var products = Enumerable.Repeat(ProductDto, 3);

        var saleService = new Mock<ISaleService>();
        saleService.Setup(ps => ps.GetDiscount(It.IsAny<List<Product>>())).Returns(10);
        var productService = new Mock<IProductService>();

        var saleController = new SaleController(saleService.Object, productService.Object);
        var result = saleController.GetDiscount(products.Select(p => p.Id).ToList()) as OkObjectResult;
        
        const double expected = 10;
        var discount = result.Value as SaleDiscountDTO;
        Assert.IsNotNull(result);
        Assert.AreEqual(expected, discount.discountPrice);
    }
    
}

    
