using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service.Sale;
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
}