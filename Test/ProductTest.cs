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
    Product testProduct = new Product();
    [TestMethod]
    public void CreateProductOk()
    {
        Assert.IsNotNull(testProduct);
    }
    [TestMethod]
    public void ProductHasName()
    {
        Product aProduct = new Product("Juan", "Está godines");
        Assert.AreEqual("Juan", aProduct.Name);
    }
    [TestMethod]
    public void ProductHasDescription()
    {
        Product aProduct = new Product("Juan", "Está godines");
        aProduct.Description = "Es bueno";
        Assert.AreEqual("Es bueno", aProduct.Description);
    }
    [TestMethod]
    public void ProductHasPrice()
    {
        Product aProduct = new Product("Juan", "Está godines",10);
        aProduct.Price = 500;
        Assert.AreEqual(500, aProduct.Price);
    }

}
