using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service.Sale;
using Service.User;
using Service.Product;
using Moq;
using System;
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
        // Arrange
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

        // Assert
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

}