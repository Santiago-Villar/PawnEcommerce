
using Microsoft.AspNetCore.Mvc;
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
    
    [TestMethod]
    public void GetAll_Ok()
    {
        var product1 = new Product { Name = "product1" };
        var products = Enumerable.Repeat(product1, 3).ToArray();
       
        var productService = new Mock<IProductService>();
        productService.Setup(ps => ps.GetAllProducts()).Returns(products);
       
        var productController = new ProductController(productService.Object);
        var result = productController.GetAll() as OkObjectResult;
        
        Assert.IsNotNull(result);
        CollectionAssert.AreEqual(products, result.Value as Array);
    }
}