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
using Microsoft.Extensions.DependencyInjection;
using Service.Session;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Service.User;

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
        Description = "Est� godines",
        Price = 10,
        Stock = 100,
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

    private readonly SaleCreationModel _newSale = new SaleCreationModel()
    {
        ProductIds = new int[]
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
        sessionServiceMock.Setup(s => s.GetCurrentUser()).Returns(new User { Id = 123 });

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
    public void CreateSale_Successful_ReturnsOkWithEmptyCartMessage()
    {
        var userId = 1;
        var fakeToken = "Bearer fake_token";

        var user = new User { Id = userId };
        sessionServiceMock.Setup(ss => ss.GetCurrentUser()).Returns(user);

        var productServiceMock = new Mock<IProductService>();
        productServiceMock.Setup(ps => ps.Get(It.IsAny<int>())).Returns(aProduct);
        productServiceMock.Setup(ps => ps.VerifyAndUpdateCart(It.IsAny<Product[]>()))
            .Returns((new Product[] { aProduct }, new List<Product>()));

        var saleServiceMock = new Mock<ISaleService>();
        saleServiceMock.Setup(ss => ss.Create(It.IsAny<Sale>())).Returns(1);

        var saleController = new SaleController(saleServiceMock.Object, productServiceMock.Object, serviceProviderMock.Object);

        saleController.ControllerContext = new ControllerContext
        {
            HttpContext = httpContextMock.Object
        };

        var actionResult = saleController.Create(_newSale);

        Assert.IsNotNull(actionResult, "No action result returned");

        Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult), $"Unexpected action result type: {actionResult.GetType().Name}");

        var okResult = actionResult as OkObjectResult;
        Assert.IsNotNull(okResult, "Expected OkObjectResult");
        Assert.IsNotNull(okResult.Value, "Ok result has no value");

        var valueType = okResult.Value.GetType();
        var messageProperty = valueType.GetProperty("Message");
        Assert.IsNotNull(messageProperty, "The 'Message' property is not found on the result value");

        var messageValue = messageProperty.GetValue(okResult.Value);
        Assert.IsNotNull(messageValue, "The 'Message' property should not be null");
        Assert.IsInstanceOfType(messageValue, typeof(string), "The 'Message' property should be a string");

        Assert.AreEqual("Sale created successfully", messageValue.ToString(), "Unexpected message content");
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
        foundSaleWithId.UserId = 123;

        var saleService = new Mock<ISaleService>();
        saleService.Setup(ps => ps.Get(1)).Returns(foundSaleWithId);
        var productService = new Mock<IProductService>();

        var saleController = new SaleController(saleService.Object, productService.Object, serviceProviderMock.Object);

        var result = saleController.Get(1) as OkObjectResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(foundSaleWithId, result.Value);
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


