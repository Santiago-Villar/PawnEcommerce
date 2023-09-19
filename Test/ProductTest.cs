using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Service;
using Service.Product;

namespace Test;

[TestClass]
public class ProductTest
{
    Product aProduct = new Product("Juan", "Está godines", 10);
    [TestMethod]
    public void CreateProductOk()
    {
        Assert.IsNotNull(aProduct);
    }
    [TestMethod]
    public void ProductHasName()
    {
        
        Assert.AreEqual("Juan", aProduct.Name);
    }

    [TestMethod]
    public void ProductHasDescription()
    {
        aProduct.Description = "Es bueno";
        Assert.AreEqual("Es bueno", aProduct.Description);
    }

    [TestMethod]
    public void ProductHasPrice()
    {
        aProduct.Price = 500;
        Assert.AreEqual(500, aProduct.Price);
    }

    [TestMethod]
    [ExpectedException(typeof(ServiceException))]
    public void ProductHasNegativePrice()
    {
        aProduct.Price = -5;
    }

    [TestMethod]
    public void ProductHasPositivePrice()
    {
        aProduct.Price = 50;
        Assert.IsTrue(aProduct.Price > 0);
    }

}
