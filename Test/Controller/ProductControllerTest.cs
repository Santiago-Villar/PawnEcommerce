
using Microsoft.AspNetCore.Mvc;
using Moq;
using PawnEcommerce.Controllers;
using PawnEcommerce.DTO.Product;
using Service.Product;

namespace Test.Controller;

[TestClass]
public class ProductControllerTest
{
    private readonly  ProductDTO _productDto = new()
    {
        Name = "testProd",
        Description = "test description",
        Price = 10,
        BrandName = "none",
        CategoryName = "none",
        Colors = new[] { "blue", "red" }
    };

    private readonly Product _product1 = new() { Name = "product1" };
    
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
        var products = Enumerable.Repeat(_product1, 3).ToArray();
       
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
        var productService = new Mock<IProductService>();
        productService.Setup(ps => ps.GetProductByName("product1")).Returns(_product1);
       
        var productController = new ProductController(productService.Object);
        var result = productController.Get("product1") as OkObjectResult;
        
        Assert.IsNotNull(result);
        Assert.AreEqual(_product1, result.Value);
    }
    
    [TestMethod]
    public void Create_Ok()
    {
        var productService = new Mock<IProductService>();
       
        var productController = new ProductController(productService.Object);
        var result = productController.Create(_productDto) as OkResult;
        
        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);
    }
    
    [TestMethod]
    public void Update_Ok()
    {
        var productService = new Mock<IProductService>();
       
        var productController = new ProductController(productService.Object);
        var result = productController.Update(_productDto) as OkResult;
        
        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);
    }
    
    [TestMethod]
    public void Delete_Ok()
    {
        var productService = new Mock<IProductService>();
       
        var productController = new ProductController(productService.Object);
        var result = productController.Delete(_productDto) as OkResult;
        
        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);
    }
}