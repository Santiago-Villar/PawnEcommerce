
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using Moq;
using PawnEcommerce.Controllers;
using PawnEcommerce.DTO.Product;
using Service.Filter.ConcreteFilter;
using Service.Product;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace Test.Controller;

[TestClass]
[ExcludeFromCodeCoverage]
public class ProductControllerTest
{
    private readonly  ProductCreationModel _productCreationModel = new()
    {
        Name = "testProd",
        Description = "test description",
        Price = 10,
        BrandId = 1,
        CategoryId = 1,
        Colors = new int[] { 1, 2 }
    };

    private readonly Product _product1 = new() { Name = "product1" };
    
    [TestMethod]
    public void CanCreateController_Ok()
    {
        var productSerivce = new Mock<IProductService>();
        var categoryService = new Mock<ICategoryService>();
        var brandService = new Mock<IBrandService>();
        var colorService = new Mock<IColorService>();

        var productController = new ProductController(productSerivce.Object, categoryService.Object, brandService.Object, colorService.Object);
        Assert.IsNotNull(productController);
    }
    
    [TestMethod]
    public void GetAll_Ok()
    {
        var products = Enumerable.Repeat(_product1, 3).ToArray();
       
        var productService = new Mock<IProductService>();
        productService.Setup(ps => ps.GetAllProducts(It.IsAny<FilterQuery>())).Returns(products);

        var categoryService = new Mock<ICategoryService>();
        var brandService = new Mock<IBrandService>();
        var colorService = new Mock<IColorService>();

        var productController = new ProductController(productService.Object, categoryService.Object, brandService.Object, colorService.Object);
        var result = productController.GetAll(null, null, null) as OkObjectResult;
        
        Assert.IsNotNull(result);
        CollectionAssert.AreEqual(products, result.Value as Array);
    }
    
    [TestMethod]
    public void GetByName_Ok()
    {
        var productService = new Mock<IProductService>();
        productService.Setup(ps => ps.Get(1)).Returns(_product1);

        var categoryService = new Mock<ICategoryService>();
        var brandService = new Mock<IBrandService>();
        var colorService = new Mock<IColorService>();

        var productController = new ProductController(productService.Object, categoryService.Object, brandService.Object, colorService.Object);
        var result = productController.Get(1) as OkObjectResult;
        
        Assert.IsNotNull(result);
        Assert.AreEqual(_product1, result.Value);
    }
    
    [TestMethod]
    public void Create_Ok()
    {
        var productService = new Mock<IProductService>();
        var categoryService = new Mock<ICategoryService>();
        var brandService = new Mock<IBrandService>();
        var colorService = new Mock<IColorService>();

        var productController = new ProductController(productService.Object, categoryService.Object, brandService.Object, colorService.Object);
        var result = productController.Create(_productCreationModel) as OkResult;
        
        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);
    }
    
    [TestMethod]
    public void Update_Ok()
    {
        var productService = new Mock<IProductService>();
        var categoryService = new Mock<ICategoryService>();
        var brandService = new Mock<IBrandService>();
        var colorService = new Mock<IColorService>();

        var productController = new ProductController(productService.Object, categoryService.Object, brandService.Object, colorService.Object);
        var result = productController.Update(1, _productCreationModel) as OkResult;
        
        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);
    }
    
    [TestMethod]
    public void Delete_Ok()
    {
        var productService = new Mock<IProductService>();
        var categoryService = new Mock<ICategoryService>();
        var brandService = new Mock<IBrandService>();
        var colorService = new Mock<IColorService>();

        var productController = new ProductController(productService.Object, categoryService.Object, brandService.Object, colorService.Object);
        var result = productController.Delete(1) as OkResult;
        
        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);
    }

    [TestMethod]
    public void IncreaseStock_Ok()
    {
        var productService = new Mock<IProductService>();
        productService.Setup(ps => ps.IncreaseStock(1, 5)).Verifiable(); // Verifiable ensures that the method is called

        var categoryService = new Mock<ICategoryService>();
        var brandService = new Mock<IBrandService>();
        var colorService = new Mock<IColorService>();

        var productController = new ProductController(productService.Object, categoryService.Object, brandService.Object, colorService.Object);
        var result = productController.IncreaseStock(1, 5) as OkResult;

        productService.Verify(); // Verify that the productService.IncreaseStock method was called

        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);
    }

}