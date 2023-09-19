using Service.Product;
using Service.Product.Color;

namespace Service.Promotion.ConcreteStrategies;

public class TotalLook : IPromotionStrategy
{
    private const int MinCategoryCount = 3;

    public double GetDiscountPrice(List<IProduct> products)
    {
        var totalPrice = products.Sum(product => product.Price);
        var mostExpensiveProduct = FindMostExpensiveProductWithSharedColor(products);
        var mostExpensivePrice = mostExpensiveProduct?.Price ?? 0;

        return totalPrice - mostExpensivePrice;
    }

    private static IProduct? FindMostExpensiveProductWithSharedColor(List<IProduct> products)
    {
        var colorCounts = products
            .SelectMany(product => product.Colors)
            .GroupBy(color => color)
            .ToDictionary(group => group.Key, group => group.Count());


        var targetColor = colorCounts
            .Where(kvp => kvp.Value >= MinCategoryCount)
            .OrderByDescending(kvp => GetPriceForColor(products, kvp.Key))
            .FirstOrDefault().Key;

        if (targetColor == null)
            return null;

        var mostExpensiveProduct = products
            .Where(product => product.Colors.Contains(targetColor))
            .MaxBy(product => product.Price);

        return mostExpensiveProduct;
    }

    private static decimal GetPriceForColor(List<IProduct> products, IColor color)
    {
        return products
            .Where(product => product.Colors.Contains(color))
            .Sum(product => product.Price);
    }

}