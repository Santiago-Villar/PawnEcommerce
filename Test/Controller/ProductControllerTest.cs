
using Microsoft.AspNetCore.Mvc;
using Moq;
using PawnEcommerce.Controllers;
using PawnEcommerce.DTO.Product;
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
    
    [TestMethod]
    public void GetByName_Ok()
    {
        var product1 = new Product { Name = "product1" };
        
        var productService = new Mock<IProductService>();
        productService.Setup(ps => ps.GetProductByName("product1")).Returns(product1);
       
        var productController = new ProductController(productService.Object);
        var result = productController.Get("product1") as OkObjectResult;
        
        Assert.IsNotNull(result);
        Assert.AreEqual(product1, result.Value);
    }
    
    [TestMethod]
    public void Create_Ok()
    {
        var dto = new ProductDTO()
        {
            Name = "testProd",
            Description = "test description",
            Price = 10,
            BrandName = "none",
            CategoryName = "none",
            Colors = new[] { "blue", "red" }
        };
        
        var productService = new Mock<IProductService>();
       
        var productController = new ProductController(productService.Object);
        var result = productController.Create(dto) as OkResult;
        
        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);
    }
}