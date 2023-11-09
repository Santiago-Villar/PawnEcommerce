using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Service;
using Service.Exception;
using Service.Product;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace Test;

[TestClass]
[ExcludeFromCodeCoverage]
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
        Stock = 5,
        PaymentMethod = "Mastercard",
        CategoryId = aCategory.Id,
        BrandId = aBrand.Id,
        ProductColors = new List<ProductColor>()
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
    public void ProductHasStock()
    {
        Assert.IsTrue(aProduct.Stock > 0);
        Assert.AreEqual(aProduct.Stock, 5);
    }

    [TestMethod]
    [ExpectedException(typeof(ModelException))]
    public void ProductHasNegativeStock()
    {
        aProduct.Stock = -5;
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
    [ExpectedException(typeof(ServiceException))]
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
        Assert.AreEqual(aProduct.ProductColors.Count, 2);
    }
    [TestMethod]
    public void AddDuplicateColor()
    {
        // Create a new product
        var freshProduct = new Product
        {
            Name = "Fresh Test Product",
            Price = 150,
            Category = new Category(3) { Name = "Fresh Category" },
            Brand = new Brand(3) { Name = "Fresh Brand" },
            ProductColors = new List<ProductColor>()
        };

        // Create new color objects
        var redColor = new Color(10) { Name = "Red" };
        var blueColor = new Color(11) { Name = "Blue" };

        // Add colors to product
        freshProduct.AddColor(redColor);
        freshProduct.AddColor(blueColor);
        freshProduct.AddColor(redColor);  // Intentionally adding a duplicate color

        // Check if the count of colors in the product is 2
        Assert.AreEqual(freshProduct.ProductColors.Count, 2);  // We expect only 2 unique colors
    }

    [TestMethod]
    public void IncreaseStock_Ok()
    {
        var freshProduct = new Product
        {
            Name = "Fresh Test Product",
            Price = 150,
            Stock = 20,
            Category = new Category(3) { Name = "Fresh Category" },
            Brand = new Brand(3) { Name = "Fresh Brand" },
            ProductColors = new List<ProductColor>()
        };
        freshProduct.IncreaseStock(20);
        Assert.AreEqual(freshProduct.Stock, 40);
    }

    [TestMethod]
    [ExpectedException(typeof(ModelException))]
    public void IncreaseNegativeStock()
    {
        var freshProduct = new Product
        {
            Name = "Fresh Test Product",
            Price = 150,
            Stock = 20,
            Category = new Category(3) { Name = "Fresh Category" },
            Brand = new Brand(3) { Name = "Fresh Brand" },
            ProductColors = new List<ProductColor>()
        };
        freshProduct.IncreaseStock(-20);
    }

    [TestMethod]
    public void DecreaseStock_Ok()
    {
        var freshProduct = new Product
        {
            Name = "Fresh Test Product",
            Price = 150,
            Stock = 20,
            Category = new Category(3) { Name = "Fresh Category" },
            Brand = new Brand(3) { Name = "Fresh Brand" },
            ProductColors = new List<ProductColor>()
        };
        freshProduct.DecreaseStock(20);
        Assert.AreEqual(freshProduct.Stock, 0);
    }

    [TestMethod]
    [ExpectedException(typeof(ModelException))]
    public void DecreaseNegativeStock()
    {
        var freshProduct = new Product
        {
            Name = "Fresh Test Product",
            Price = 150,
            Stock = 20,
            Category = new Category(3) { Name = "Fresh Category" },
            Brand = new Brand(3) { Name = "Fresh Brand" },
            ProductColors = new List<ProductColor>()
        };
        freshProduct.DecreaseStock(-20);
    }


    [TestMethod]
    public void ProductHasEnoughStock_Ok()
    {
        var freshProduct = new Product
        {
            Name = "Fresh Test Product",
            Price = 150,
            Stock = 20,
            Category = new Category(3) { Name = "Fresh Category" },
            Brand = new Brand(3) { Name = "Fresh Brand" },
            ProductColors = new List<ProductColor>()
        };
        Assert.IsTrue(freshProduct.IsStockAvailable(20));
    }

    [TestMethod]
    public void ProductDoesNotHaveEnoughStock()
    {
        var freshProduct = new Product
        {
            Name = "Fresh Test Product",
            Price = 150,
            Stock = 20,
            Category = new Category(3) { Name = "Fresh Category" },
            Brand = new Brand(3) { Name = "Fresh Brand" },
            ProductColors = new List<ProductColor>()
        };
        Assert.IsFalse(freshProduct.IsStockAvailable(21));
    }





}
