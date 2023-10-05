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
        
        Assert.AreEqual("Three For One", sale.PromotionName);
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
}