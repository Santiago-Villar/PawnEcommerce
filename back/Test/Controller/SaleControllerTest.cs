using Microsoft.AspNetCore.Mvc;
using Moq;
using PawnEcommerce.Controllers;
using PawnEcommerce.DTO.Product;
using PawnEcommerce.DTO.Sale;
using Service.Product;
using Service.Promotion;
using Service.Promotion.ConcreteStrategies;
using Service.Sale;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Service.Session;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

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

    private Mock<IServiceProvider> serviceProviderMock;
    private Mock<IServiceScope> serviceScopeMock;
    private Mock<IServiceScopeFactory> serviceScopeFactoryMock;
    private Mock<ISessionService> sessionServiceMock;
    private Mock<HttpRequest> httpRequestMock;
    private Mock<HttpContext> httpContextMock;

    [TestInitialize]  
    public void Setup()
    {
        serviceProviderMock = new Mock<IServiceProvider>();

        serviceScopeMock = new Mock<IServiceScope>();

        serviceScopeFactoryMock = new Mock<IServiceScopeFactory>();

        serviceProviderMock.Setup(sp => sp.GetService(typeof(IServiceScopeFactory)))
            .Returns(serviceScopeFactoryMock.Object);

        serviceScopeFactoryMock.Setup(ssf => ssf.CreateScope())
            .Returns(serviceScopeMock.Object);

        sessionServiceMock = new Mock<ISessionService>();
        sessionServiceMock.Setup(ss => ss.ExtractUserIdFromToken(It.IsAny<string>()))
            .Returns((string token) => 1);

        serviceScopeMock.Setup(scope => scope.ServiceProvider.GetService(typeof(ISessionService)))
            .Returns(sessionServiceMock.Object);

        httpRequestMock = new Mock<HttpRequest>();
        httpRequestMock.Setup(req => req.Headers).Returns(
            new HeaderDictionary(new Dictionary<string, StringValues> { { "Authorization", "Bearer fake_token" } })
        );

        httpContextMock = new Mock<HttpContext>();
        httpContextMock.Setup(ctx => ctx.Request).Returns(httpRequestMock.Object);


    }


    [TestMethod]
    public void CanCreateController_Ok()
    {
        var saleService = new Mock<ISaleService>();
        var productService = new Mock<IProductService>();

        var saleController = new SaleController(saleService.Object, productService.Object,serviceProviderMock.Object);
        Assert.IsNotNull(saleController);
    }

    [TestMethod]
    public void Create_Ok()
    {
        var saleService = new Mock<ISaleService>();
        var productService = new Mock<IProductService>();
        productService.Setup(ps => ps.Get(It.IsAny<int>())).Returns(new Product());

        var saleController = new SaleController(saleService.Object, productService.Object, serviceProviderMock.Object);

        saleController.ControllerContext = new ControllerContext
        {
            HttpContext = httpContextMock.Object
        };

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

        var saleController = new SaleController(saleService.Object, productService.Object,serviceProviderMock.Object);
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

        var saleController = new SaleController(saleService.Object, productService.Object, serviceProviderMock.Object);

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

        var saleController = new SaleController(saleService.Object, productService.Object, serviceProviderMock.Object);

        var result = saleController.GetDiscount(products.Select(p => p.Id).ToList()) as OkObjectResult;
        
        const double expected = 10;
        var discount = result.Value as SaleDiscountDTO;
        Assert.IsNotNull(result);
        Assert.AreEqual(expected, discount.discountPrice);
    }


    [TestMethod]
    public void GetCurrentUserSales_ReturnsSales_Ok()
    {
        var userIdFromToken = 1;
        var salesForUser = Enumerable.Repeat(_newSale, 3).Select(sale => sale.ToEntity()).ToList();

        var saleService = new Mock<ISaleService>();
        saleService.Setup(ss => ss.GetSalesByUserId(userIdFromToken)).Returns(salesForUser);
        var productService = new Mock<IProductService>();

        var saleController = new SaleController(saleService.Object, productService.Object, serviceProviderMock.Object);

        saleController.ControllerContext = new ControllerContext
        {
            HttpContext = httpContextMock.Object
        };

        var result = saleController.GetUserPurchaseHistory() as OkObjectResult;

        Assert.IsNotNull(result);
        CollectionAssert.AreEqual(salesForUser, result.Value as List<Sale>);
    }




}


