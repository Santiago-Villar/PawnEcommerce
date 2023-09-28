using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service.Sale;
using Service.User;
using Service.Product;
using Moq;
using System;
using Service.Promotion;
using Test.Service.Promotion;
using System.ComponentModel.DataAnnotations;

namespace Test;

[TestClass]
public class SaleTest
{

    [TestMethod]
    public void CanCreateSale_Ok()
    {
        var s = new Sale();
        Assert.IsNotNull(s);
    }

    [TestMethod]
    public void SaleHasUser()
    {

        User plainUser = new User()
        {
            Email = "diegoalmenara@gmail.com"
        };
        var s = new Sale()
        {
            User = plainUser,
            UserEmail=plainUser.Email,
        };
        Assert.AreEqual(s.UserEmail, "diegoalmenara@gmail.com");
    }
    [TestMethod]
    public void SaleContainsCorrectNumberOfAndSpecificProducts()
    {
        var product1 = new Product { Name = "Product1" };
        var product2 = new Product { Name = "Product2" };

        var saleProduct1 = new SaleProduct { Product = product1 };
        var saleProduct2 = new SaleProduct { Product = product2 };

        var mockSaleProducts = new List<SaleProduct>
    {
        saleProduct1,
        saleProduct2
    };

        var sale = new Sale
        {
            Products = mockSaleProducts
        };

        Assert.AreEqual(2, sale.Products.Count);
        Assert.AreEqual("Product1", sale.Products.ElementAt(0).Product.Name);
        Assert.AreEqual("Product2", sale.Products.ElementAt(1).Product.Name);
    }



    [TestMethod]
    public void SaleHasDate()
    {
        var s = new Sale();
        Assert.IsTrue((DateTime.Now - s.Date).TotalSeconds < 1);
    }
    [TestMethod]
    public void SaleHasPrice()
    {
        var product1Mock = new Mock<IProduct>();
        product1Mock.Setup(p => p.Name).Returns("Product1");
        product1Mock.Setup(p => p.Price).Returns(15);

        var product2Mock = new Mock<IProduct>();
        product2Mock.Setup(p => p.Name).Returns("Product2");
        product2Mock.Setup(p => p.Price).Returns(20);

        var mockProducts = new List<IProduct>
        {
            product1Mock.Object,
            product2Mock.Object
        };
    
        var s = new Sale
        {
            Products = mockProducts
        };
    
        Assert.AreEqual(s.Price, 35);
    }
    
    [TestMethod]
    public void SaleHasPromotionName()
    {
        var product1Mock = PromotionTestHelper.CreateMockProduct();

        var mockProducts = new List<IProduct>
        {
            product1Mock.Object,
            product1Mock.Object,
            product1Mock.Object
        };

        var s = new Sale
        {
            Products = mockProducts
        };

        var promotionService = new PromotionService();
        var promotion = promotionService.GetPromotion(s.Products);

        s.PromotionName = promotion.Name;

        Assert.AreEqual(s.PromotionName, promotion.Name);
    }

}