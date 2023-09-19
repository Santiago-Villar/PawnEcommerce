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
   public static Brand aBrand = new Brand("Kova");
    public static Category aCategory = new Category("Retro");
    public static Product aProduct = new Product("Juan", "Está godines", 10,aCategory,aBrand);
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

    [TestMethod]
    public void ProductHasCategory()
    {
        aProduct.Category = new Category("Casual");
        Assert.IsNotNull(aProduct.Category);
    }
    [TestMethod]
    public void ProductHasBrand()
    {
        Assert.IsNotNull(aProduct.Brand);
    }
    [TestMethod]
    public void ProductBrandOk()
    {
        Assert.AreEqual(aProduct.Brand.Name, "Kova");
    }

    [TestMethod]
    public void ProductBrandFails()
    {
        Assert.AreNotEqual(aProduct.Brand.Name, "Puma");
    }

    [TestMethod]
    public void ProductColorsAreNotNull()
    {
        Assert.IsNotNull(aProduct.Colors);
    }
    [TestMethod]
    [ExpectedException(typeof(ServiceException))]
    public void AddEmptyColor()
    {
        aProduct.AddColor("");
    }
    [TestMethod]
    public void AddColorOk()
    {
        aProduct.AddColor("Green");
        aProduct.AddColor("Red");
        Assert.AreEqual(aProduct.Colors.Count, 2);
    }
    [TestMethod]
    public void AddDuplicateColor()
    {
        aProduct.AddColor("Green");
        aProduct.AddColor("Red");
        aProduct.AddColor("Green");
        Assert.AreEqual(aProduct.Colors.Count, 2);
    }

}
