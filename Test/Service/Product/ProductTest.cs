using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Service;
using Service.Exception;
using Service.Product;

namespace Test;

[TestClass]
public class ProductTest
{
    public static Brand aBrand = new Brand()
    {
        Name = "Kova"
    };
    public static Category aCategory = new Category()
    {
        Name= "Retro"
    };

    public static Product aProduct = new Product()
    {
        Name = "Abdul's Udemy Course",
        Description = "Está godines",
        Price = 10,
        Category = aCategory,
        Brand = aBrand,
        CategoryName = aCategory.Name,  // set foreign key property
        BrandName = aBrand.Name,        // set foreign key property
        Colors = new List<Color>()
    };
    Color firstColor = new Color()
    {
        Name = "Red"
    };
    Color secondColor = new Color()
    {
        Name = "Green"
    };
    Color thirdColor = new Color()
    {
        Name = "Blue"
    };
    [TestMethod]
    public void CreateProductOk()
    {
        Assert.IsNotNull(aProduct);
    }
    [TestMethod]
    public void ProductHasName()
    {
        
        Assert.AreEqual("Abdul's Udemy Course", aProduct.Name);
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
    [ExpectedException(typeof(ModelException))]
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
        aProduct.Category = new Category()
        {
            Name = "Casual"
        };
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
    [ExpectedException(typeof(ModelException))]
    public void AddEmptyColor()
    {
        Color emptyColor=new Color()
        {
            Name=""
        };
        aProduct.AddColor(emptyColor);
    }
    [TestMethod]
    public void AddColorOk()
    {

        aProduct.AddColor(firstColor);
        aProduct.AddColor(secondColor);
        Assert.AreEqual(aProduct.Colors.Count, 2);
    }
    [TestMethod]
    public void AddDuplicateColor()
    {
        aProduct.AddColor(firstColor);
        aProduct.AddColor(thirdColor);
        Assert.AreEqual(aProduct.Colors.Count, 3);
    }

}
