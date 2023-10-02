
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using Moq;
using PawnEcommerce.Controllers;
using PawnEcommerce.DTO.Product;
using Service.Filter.ConcreteFilter;
using Service.Product;

namespace Test.Controller;

[TestClass]
public class ProductControllerTest
{
    private readonly  ProductCreationModel _productCreationModel = new()
    {
        Name = "testProd",
        Description = "test description",
        Price = 10,
        Brand = new BrandDTO() {Name = "none", Id = 1},
        Category = new CategoryDTO() {Name = "none", Id = 1},
        Colors = new ColorDTO[] { 
            new ColorDTO(){Id = 1, Name = "blue"},
            new ColorDTO(){Id = 2, Name = "red"}, 
        }
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
        productService.Setup(ps => ps.GetAllProducts(It.IsAny<FilterQuery>())).Returns(products);
       
        var productController = new ProductController(productService.Object);
        var result = productController.GetAll(null, null, null) as OkObjectResult;
        
        Assert.IsNotNull(result);
        CollectionAssert.AreEqual(products, result.Value as Array);
    }
    
    [TestMethod]
    public void GetByName_Ok()
    {
        var productService = new Mock<IProductService>();
        productService.Setup(ps => ps.Get(1)).Returns(_product1);
       
        var productController = new ProductController(productService.Object);
        var result = productController.Get(1) as OkObjectResult;
        
        Assert.IsNotNull(result);
        Assert.AreEqual(_product1, result.Value);
    }
    
    [TestMethod]
    public void Create_Ok()
    {
        var productService = new Mock<IProductService>();
       
        var productController = new ProductController(productService.Object);
        var result = productController.Create(_productCreationModel) as OkResult;
        
        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);
    }
    
    [TestMethod]
    public void Update_Ok()
    {
        var productService = new Mock<IProductService>();
       
        var productController = new ProductController(productService.Object);
        var result = productController.Update(_productCreationModel) as OkResult;
        
        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);
    }
    
    [TestMethod]
    public void Delete_Ok()
    {
        var productService = new Mock<IProductService>();
       
        var productController = new ProductController(productService.Object);
        var result = productController.Delete(1) as OkResult;
        
        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);
    }
}