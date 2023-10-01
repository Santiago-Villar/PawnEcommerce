using Moq;
using PawnEcommerce.Controllers;
using Service.Sale;

namespace Test.Controller;

[TestClass]
public class SaleControllerTest
{
    [TestMethod]
    public void CanCreateController_Ok()
    {
        var saleService = new Mock<ISaleService>();
        var saleController = new SaleController(saleService.Object);
        Assert.IsNotNull(saleController);
    }
    
}