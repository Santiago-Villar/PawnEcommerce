
using Moq;
using PawnEcommerce.Controllers;
using Service.Product;

namespace Test.Controller;

[TestClass]
public class ProductControllerTest
{
    [TestMethod]
    public void CanCreateController_Ok()
    {
        var productSerivce = new Mock<IProductService>();
        var productController = new ProductController(productSerivce.Object);
        Assert.IsNotNull(productController);
    }
}