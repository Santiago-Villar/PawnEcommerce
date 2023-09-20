using Service.Product;

namespace Service.Promotion.ConcreteStrategies;

public class ThreeForTwo : IPromotionStrategy
{
    public string Name { get; init; } = "Three For Two";

    private const int MinCategoryCount = 3;
    public double GetDiscountPrice(List<IProduct> products)
    {
        var totalPrice = products.Sum(product => product.Price);
        var cheapestProduct = FindCheapestProductInCommonCategories(products);
        var cheapestPrice = cheapestProduct?.Price ?? 0;
        
        return totalPrice - cheapestPrice;
    }
    
    private static IProduct? FindCheapestProductInCommonCategories(List<IProduct> products)
    {
        var categoryCounts = FindCategoryCount(products);

        if (categoryCounts.Count == 0)
            return null;

        var cheapestProduct = FindCheapestProduct(products, categoryCounts);

        return cheapestProduct;
    }

    private static IProduct? FindCheapestProduct(List<IProduct> products, List<ICategory> categoryCounts)
    {
        return products
            .Where(product => categoryCounts
                .Contains(product.Category))
            .MinBy(product => product.Price);
    }

    private static List<ICategory> FindCategoryCount(List<IProduct> products)
    {
        return products
            .GroupBy(product => product.Category)
            .Where(group => group.Count() >= MinCategoryCount)
            .Select(group => group.Key)
            .ToList();
    }
}