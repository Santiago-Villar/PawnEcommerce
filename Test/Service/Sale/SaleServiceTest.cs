using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service.Sale;
using Service.User;
using Service.Product;
using Moq;
using System;
using Service.Exception;
using Service.Promotion;
using Test.Service.Promotion;
namespace Test.Service;

[TestClass]
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
        var product1Mock = PromotionTestHelper.CreateMockProduct();
        var mockUser = new Mock<IUser>();
        mockUser.Setup(user => user.Email).Returns("testEmail");
        
        var sale = new Sale
        {
            User = mockUser.Object,
            Products = Enumerable.Repeat(product1Mock.Object, 3).ToList()
        };

        var saleList = new List<Sale>() { sale };
        var mockRepository = new Mock<ISaleRepository>();
        mockRepository.Setup(repo => repo.GetUserSales(mockUser.Object)).Returns(saleList);
        
        var saleService = new SaleService(mockRepository.Object);
        saleService.Create(sale);
        
        Assert.AreEqual("Three For One", sale.PromotionName);
    }
    
    [TestMethod]
    public void CanCreateSale_NoPromotionAvailable_Ok()
    {
        var product1Mock = PromotionTestHelper.CreateMockProduct();
        var mockUser = new Mock<IUser>();
        mockUser.Setup(user => user.Email).Returns("testEmail");
        
        var sale = new Sale
        {
            User = mockUser.Object,
            Products = Enumerable.Repeat(product1Mock.Object, 1).ToList()
        };

        var saleList = new List<Sale>() { sale };
        var mockRepository = new Mock<ISaleRepository>();
        mockRepository.Setup(repo => repo.GetUserSales(mockUser.Object)).Returns(saleList);
        
        var saleService = new SaleService(mockRepository.Object);
        saleService.Create(sale);
        
        Assert.IsNull(sale.PromotionName);
    }
    
    [ExpectedException(typeof(ServiceException))]
    [TestMethod]
    public void CreateSale_NoProducts_Throw()
    {
        var mockUser = new Mock<IUser>();
        mockUser.Setup(user => user.Email).Returns("testEmail");
        
        var sale = new Sale
        {
            User = mockUser.Object,
        };

        var saleList = new List<Sale>() { sale };
        var mockRepository = new Mock<ISaleRepository>();
        mockRepository.Setup(repo => repo.GetUserSales(mockUser.Object)).Returns(saleList);
        
        var saleService = new SaleService(mockRepository.Object);
        saleService.Create(sale);
    }
    

}