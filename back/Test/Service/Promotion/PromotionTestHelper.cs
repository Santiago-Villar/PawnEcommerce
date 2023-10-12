using Service.Product;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace Test.Service.Promotion;

[ExcludeFromCodeCoverage]
public class PromotionTestHelper
{
    public static Product CreateProduct()
    {
        const int price = 10;
        const string categoryName = "Jeans";
        const string colorName = "Blue";
        const string brandName = "Zara";

        var color = new Color(2) { Name = colorName };
        var productColors = Enumerable.Repeat(new ProductColor { Color = color }, 3).ToList();

        var category = new Category(2) { Name = categoryName };
        var brand = new Brand(2) { Name = brandName };

        return new Product
        {
            Category = category,
            Price = price,
            ProductColors = productColors, 
            Brand = brand
        };
    }


    public static Product CreateProduct(Category category, List<Color> colors, Brand brand)
    {
        const int price = 10;

        var productColors = colors.Select(c => new ProductColor { Color = c }).ToList();

        return new Product
        {
            Category = category,
            Price = price,
            ProductColors = productColors,  // This is the change
            Brand = brand
        };
    }


    public static Category CreateCategory(string name)
    {
        return new Category(9) { Name = name };
    }

    public static Brand CreateBrand(string name)
    {
        return new Brand(9) { Name = name };
    }

    public static Color CreateColor(string name)
    {
        return new Color(9) { Name = name };
    }
}

