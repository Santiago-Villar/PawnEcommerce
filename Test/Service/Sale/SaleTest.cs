using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service.Sale;
using Service.User;
using Service.Product;
using Moq;
using System;
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

        var userMock = new Mock<IUser>();
        userMock.Setup(user => user.Email).Returns("diegoalmenara@gmail.com");
        var plainUser = userMock.Object;
        var s = new Sale()
        {
            User = plainUser
        };
        Assert.AreEqual(s.User.Email, "diegoalmenara@gmail.com");
    }
    [TestMethod]
    public void SaleHasProducts()
    {
        var product1Mock = new Mock<IProduct>();
        product1Mock.Setup(p => p.Name).Returns("Product1");

        var product2Mock = new Mock<IProduct>();
        product2Mock.Setup(p => p.Name).Returns("Product2");

        var mockProducts = new List<IProduct>
        {
            product1Mock.Object,
            product2Mock.Object
        };
        var s = new Sale
        {
            Products = mockProducts

        };

        Assert.AreEqual(s.Products.Count, 2);
        Assert.AreEqual(s.Products[0].Name, "Product1");
        Assert.AreEqual(s.Products[1].Name, "Product2");
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

}