
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
    private ProductCreationModel _productCreationModel;
    private Product _product1;
    private Mock<IProductService> _productService;
    private Mock<ICategoryService> _categoryService;
    private Mock<IBrandService> _brandService;
    private Mock<IColorService> _colorService;
    private ProductController _productController;

    [TestInitialize]
    public void Setup()
    {
        _productCreationModel = new ProductCreationModel
        {
            Name = "testProd",
            Description = "test description",
            Price = 10,
            BrandId = 1,
            CategoryId = 1,
            Colors = new[] { 1, 2 }
        };

        _product1 = new Product { Name = "product1" };

        _productService = new Mock<IProductService>();
        _categoryService = new Mock<ICategoryService>();
        _brandService = new Mock<IBrandService>();
        _colorService = new Mock<IColorService>();

        _productController = new ProductController(_productService.Object, _categoryService.Object, _brandService.Object, _colorService.Object);
    }

    [TestMethod]
    public void CanCreateController_Ok()
    {
        Assert.IsNotNull(_productController);
    }

    [TestMethod]
    public void GetAll_Ok()
    {
        var products = Enumerable.Repeat(_product1, 3).ToArray();
        _productService.Setup(ps => ps.GetAllProducts(It.IsAny<FilterQuery>())).Returns(products);

        var result = _productController.GetAll(null, null, null) as OkObjectResult;

        Assert.IsNotNull(result);
        CollectionAssert.AreEqual(products, result.Value as Array);
    }

    [TestMethod]
    public void GetByName_Ok()
    {
        _productService.Setup(ps => ps.Get(1)).Returns(_product1);

        var result = _productController.Get(1) as OkObjectResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(_product1, result.Value);
    }

    [TestMethod]
    public void Create_Ok()
    {
        var result = _productController.Create(_productCreationModel) as OkResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);
    }

    [TestMethod]
    public void Update_Ok()
    {
        var result = _productController.Update(1, _productCreationModel) as OkResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);
    }

    [TestMethod]
    public void Delete_Ok()
    {
        var result = _productController.Delete(1) as OkResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);
    }
}