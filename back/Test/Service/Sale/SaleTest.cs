using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service.Sale;
using Service.User;
using Service.Product;
using Moq;
using System;
using Service.Promotion;
using Test.Service.Promotion;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace Test;

[TestClass]
[ExcludeFromCodeCoverage]
public class SaleTest
{

    [TestMethod]
    public void CanCreateSale_Ok()
    {
        var s = new Sale();
        Assert.IsNotNull(s);
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
        var product1 = new Product()
        {
            Name = "Product1",
            Price = 15
        };

        var product2 = new Product()
        {
            Name = "Product2",
            Price = 20
        };

        var mockProducts = new List<IProduct>
        {
            product1,
            product2
        };

        var saleProduct1 = new SaleProduct()
        {
            Product = product1
        };
        
        var saleProduct2 = new SaleProduct()
        {
            Product = product2
        };

        var s = new Sale
        {
            Products = new List<SaleProduct>
            {
                saleProduct1,
                saleProduct2
            }
        };
    
        Assert.AreEqual(s.Price, 35);
    }
    
    [TestMethod]
    public void SaleHasPromotionName()
    {
        var product1 = PromotionTestHelper.CreateProduct();

        var saleProduct1Mock = new SaleProduct
        {
            Product = product1
        };

        var s = new Sale
        {
            Products = new List<SaleProduct>
            {
                saleProduct1Mock,
                saleProduct1Mock,
                saleProduct1Mock
            }
        };

        var promotionService = new PromotionService();
        var promotion = promotionService.GetPromotion(s.Products.Select(sp => sp.Product).ToList());

        s.PromotionName = promotion.Name;

        Assert.AreEqual(s.PromotionName, promotion.Name);
    }

}