﻿using System;
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
    public static Brand aBrand = new Brand(2)
    {
        Name = "Kova"
    };
    public static Category aCategory = new Category(2)
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
        CategoryId = aCategory.Id,
        BrandId = aBrand.Id,   
        Colors = new List<Color>()
    };
    Color firstColor = new Color(1)
    {
        Name = "Red"
    };
    Color secondColor = new Color(2)
    {
        Name = "Green"
    };
    Color thirdColor = new Color(3)
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
        aProduct.Category = new Category(4)
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
        Color emptyColor=new Color(4)
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
