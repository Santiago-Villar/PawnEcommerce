using Service.Product;

namespace Test.Service.Promotion;

public class PromotionTestHelper
{
    public static Product CreateProduct()
    {
        const int price = 10;
        const string categoryName = "Jeans";
        const string colorName = "Blue";
        const string brandName = "Zara";

        var color = new Color { Name = colorName };
        var colors = Enumerable.Repeat(color, 3).ToList();

        var category = new Category { Name = categoryName };
        var brand = new Brand(2) { Name = brandName };

        return new Product
        {
            Category = category,
            Price = price,
            Colors = colors,
            Brand = brand
        };
    }

    public static Product CreateProduct(Category category, List<Color> colors, Brand brand)
    {
        const int price = 10;

        return new Product
        {
            Category = category,
            Price = price,
            Colors = colors,
            Brand = brand
        };
    }

    public static Category CreateCategory(string name)
    {
        return new Category { Name = name };
    }

    public static Brand CreateBrand(string name)
    {
        return new Brand(9) { Name = name };
    }

    public static Color CreateColor(string name)
    {
        return new Color { Name = name };
    }
}

