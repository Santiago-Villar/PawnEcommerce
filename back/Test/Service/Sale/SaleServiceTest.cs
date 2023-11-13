using System.Runtime.CompilerServices;
using Service.Sale;
using Service.User;
using Moq;
using Service.Exception;
using Service.Product;
using Test.Service.Promotion;
namespace Test.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

[TestClass]
[ExcludeFromCodeCoverage]
public class SaleServiceTest
{
    [TestMethod]
    public void CanCreateSaleService_Ok()
    {
        var mockRepository = new Mock<ISaleRepository>();
        var saleService = new SaleService(mockRepository.Object);
        Assert.IsNotNull(saleService);
    }
    
    [TestMethod]
    public void CanCreateSale_Ok()
    {
        var product1Mock = PromotionTestHelper.CreateProduct();

        var saleProduct1Mock = new SaleProduct
        {
            Product = product1Mock
        };
        
        var sale = new Sale
        {
            Products = Enumerable.Repeat(saleProduct1Mock, 3).ToList()
        };

        var mockRepository = new Mock<ISaleRepository>();
        var saleService = new SaleService(mockRepository.Object);
        saleService.Create(sale);
        
        Assert.AreEqual("3x1 Fidelidad", sale.PromotionName);
    }
    
    [TestMethod]
    public void CanCreateSale_NoPromotionAvailable_Ok()
    {
        var product1Mock = PromotionTestHelper.CreateProduct();
        
        var saleProduct = new SaleProduct
        {
            Product = product1Mock
        };
        
        var sale = new Sale
        {
            Products = Enumerable.Repeat(saleProduct, 1).ToList()
        };

        var mockRepository = new Mock<ISaleRepository>();
        var saleService = new SaleService(mockRepository.Object);
        saleService.Create(sale);
        
        Assert.IsNull(sale.PromotionName);
        
    }
    
    [TestMethod]
    public void GetAllPromotions_Ok()
    {
        var product1Mock = PromotionTestHelper.CreateProduct();
        var user = new User
        {
            Email = "testEmail@gmail.com"
        };
        
        var saleList = CreateSales(user, product1Mock);
        
        var mockRepository = new Mock<ISaleRepository>();
        mockRepository.Setup(repo => repo.GetAll()).Returns(saleList);
        
        var saleService = new SaleService(mockRepository.Object);
        
        Assert.AreEqual(saleList, saleService.GetAll());
    }


    private List<Sale> CreateSales(User user, Product product1Mock)
    {

        var saleProduct1Mock = new SaleProduct
        {
            Product = product1Mock
        };

        var sale = new Sale
        {
            Products = Enumerable.Repeat(saleProduct1Mock, 4).ToList()
        };

        var sale2 = new Sale
        {
            Products = Enumerable.Repeat(saleProduct1Mock, 2).ToList()
        };

        var saleList = new List<Sale>() { sale, sale2 };
        return saleList;
    }

    [TestMethod]
    public void GetById_Ok()
    {
        var product1Mock = PromotionTestHelper.CreateProduct();

        var saleProduct = new SaleProduct
        {
            Product = product1Mock
        };

        var sale = new Sale
        {
            Products = Enumerable.Repeat(saleProduct, 1).ToList(),
            Id = 9
        };

        var mockRepository = new Mock<ISaleRepository>();
        mockRepository.Setup(repo => repo.Get(sale.Id)).Returns(sale);

        var saleService = new SaleService(mockRepository.Object);

        Assert.AreEqual(sale, saleService.Get(sale.Id));
    }

    [TestMethod]
    public void GetSalesByUserId_ShouldReturnCorrectSales()
    {
        var productMock = PromotionTestHelper.CreateProduct();
        var user = new User
        {
            Id = 1,
            Email = "testuser@email.com"
        };

        var saleProductMock = new SaleProduct
        {
            Product = productMock
        };

        var sale1 = new Sale
        {
            UserId = user.Id,
            Products = Enumerable.Repeat(saleProductMock, 3).ToList()
        };

        var sale2 = new Sale
        {
            UserId = user.Id,
            Products = Enumerable.Repeat(saleProductMock, 2).ToList()
        };

        var salesForUser = new List<Sale> { sale1, sale2 };

        var mockRepository = new Mock<ISaleRepository>();
        mockRepository.Setup(repo => repo.GetSalesByUserId(user.Id)).Returns(salesForUser);

        var saleService = new SaleService(mockRepository.Object);

        var result = saleService.GetSalesByUserId(user.Id);

        Assert.AreEqual(2, result.Count); // Expecting two sales for the user
        Assert.IsTrue(result.Any(s => s.UserId == user.Id));
    }

}