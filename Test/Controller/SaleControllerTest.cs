using Microsoft.AspNetCore.Mvc;
using Moq;
using PawnEcommerce.Controllers;
using PawnEcommerce.DTO.Product;
using PawnEcommerce.DTO.Sale;
using Service.Sale;

namespace Test.Controller;

[TestClass]
public class SaleControllerTest
{
    private static readonly ProductCreationModel ProductDto = new ProductCreationModel()
    {
        Name = "testProd",
        Description = "test description",
        Price = 10,
        BrandName = "none",
        CategoryName = "none",
        Colors = new ColorDTO[] 
        { 
            new ColorDTO(){Name = "blue", Id = 1},
            new ColorDTO(){Name = "red", Id = 2}
        }
    };

    private readonly SaleDTO _newSale = new SaleDTO()
    {
        UserId = 1,
        ProductDtos = new ProductCreationModel[]
        {
            ProductDto
        },
    };
    
    [TestMethod]
    public void CanCreateController_Ok()
    {
        var saleService = new Mock<ISaleService>();
        var saleController = new SaleController(saleService.Object);
        Assert.IsNotNull(saleController);
    }
    
    [TestMethod]
    public void Create_Ok()
    {
        var saleService = new Mock<ISaleService>();
        var saleController = new SaleController(saleService.Object);
        
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

        var saleController = new SaleController(saleService.Object);
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

        var saleController = new SaleController(saleService.Object);
        var result = saleController.Get(1) as OkObjectResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(foundSaleWithId, result.Value);
    }
    
}

    
