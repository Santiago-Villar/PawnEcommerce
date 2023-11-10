using Service.Product;

namespace Service.Promotion.ConcreteStrategies;

public class TotalLook : IPromotionStrategy
{
    public string Name { get; init; } = "Total Look";

    private const double FiftyPercentConverter = 0.5;
    private const int MinCategoryCount = 3;

    public double GetDiscountPrice(List<Service.Product.Product> products)
    {
        var eligibleProducts = products.Where(p => !p.IsExcludedFromPromotions).ToList();
        var totalPrice = eligibleProducts.Sum(product => product.Price);
        var mostExpensiveProduct = FindMostExpensiveProductWithSharedColor(eligibleProducts);
        var mostExpensivePrice = mostExpensiveProduct?.Price ?? 0;

        return totalPrice - mostExpensivePrice * FiftyPercentConverter;
    }


    private static Service.Product.Product? FindMostExpensiveProductWithSharedColor(List<Service.Product.Product> products)
    {
        var colorCounts = FindColorCount(products);
        
        var targetColor = FindTargetColor(products, colorCounts);

        if (targetColor == null)
            return null;

        var mostExpensiveProduct = FindMostExpensiveProduct(products, targetColor);

        return mostExpensiveProduct;
    }

    private static Service.Product.Product? FindMostExpensiveProduct(List<Service.Product.Product> products, Color targetColor)
    {
        return products
            .Where(product => product.Colors.Contains(targetColor))
            .MaxBy(product => product.Price);
    }

    private static Color? FindTargetColor(List<Service.Product.Product> products, Dictionary<Color, int> colorCounts)
    {
        return colorCounts
            .Where(kvp => kvp.Value >= MinCategoryCount)
            .OrderByDescending(kvp => GetPriceForColor(products, kvp.Key))
            .FirstOrDefault().Key;
    }

    private static Dictionary<Color, int> FindColorCount(List<Service.Product.Product> products)
    {
        return products
            .SelectMany(product => product.Colors.Distinct())
            .GroupBy(color => color)
            .Where(group => group.Count() >= MinCategoryCount)
            .ToDictionary(group => group.Key, group => group.Count());
    }

    private static decimal GetPriceForColor(List<Service.Product.Product> products, Color color)
    {
        return products
            .Where(product => product.Colors.Contains(color))
            .Sum(product => product.Price);
    }

}