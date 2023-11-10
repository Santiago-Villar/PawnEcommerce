
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using Moq;
using PawnEcommerce.Controllers;
using Service.DTO.Product;
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
    private ProductUpdateModel _productUpdateModel;
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

        _productUpdateModel = new ProductUpdateModel
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

        var result = _productController.GetAll(null, null, null,null,null) as OkObjectResult;

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
        var result = _productController.Create(_productCreationModel) as OkObjectResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);
    }

    [TestMethod]
    public void Update_OnlyName_Ok()
    {
        var expectedProduct = new Product
        {
            Id = _product1.Id,
            Name = "UpdatedName",
            Description = _product1.Description,
            Price = _product1.Price,
            BrandId = _product1.BrandId,
            CategoryId = _product1.CategoryId,
        };
        _productService.Setup(ps => ps.UpdateProductUsingDTO(It.IsAny<int>(), It.IsAny<ProductUpdateModel>()))
                       .Returns(expectedProduct);

        var updatedProductModel = new ProductUpdateModel
        {
            Name = "UpdatedName"
        };

        var result = _productController.Update(1, updatedProductModel) as OkObjectResult;
        _productService.Verify(ps => ps.UpdateProductUsingDTO(1, It.Is<ProductUpdateModel>(p => p.Name == "UpdatedName")));
        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);
        Assert.IsInstanceOfType(result.Value, typeof(Product));
    }

    [TestMethod]
    public void Delete_Ok()
    {
        var result = _productController.Delete(1) as OkResult;

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
    [TestMethod]
    public void DecreaseStock_Ok()
    {
        var productService = new Mock<IProductService>();
        productService.Setup(ps => ps.DecreaseStock(1, 3)).Verifiable(); // Verifiable ensures that the method is called

        var categoryService = new Mock<ICategoryService>();
        var brandService = new Mock<IBrandService>();
        var colorService = new Mock<IColorService>();

        var productController = new ProductController(productService.Object, categoryService.Object, brandService.Object, colorService.Object);
        var result = productController.DecreaseStock(1, 3) as OkResult;

        productService.Verify(); // Verify that the productService.DecreaseStock method was called

        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);
    }
}