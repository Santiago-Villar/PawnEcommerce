using Service.Product;

namespace Service.Promotion.ConcreteStrategies;

public class ThreeForTwo : IPromotionStrategy
{
    public string Name { get; init; } = "Three For Two";

    private const int MinCategoryCount = 3;
    public double GetDiscountPrice(List<Service.Product.Product> products)
    {
        var totalPrice = products.Sum(product => product.Price);
        var cheapestProduct = FindCheapestProductInCommonCategories(products);
        var cheapestPrice = cheapestProduct?.Price ?? 0;
        
        return totalPrice - cheapestPrice;
    }
    
    private static Service.Product.Product? FindCheapestProductInCommonCategories(List<Service.Product.Product> products)
    {
        var categoryCounts = FindCategoryCount(products);

        if (categoryCounts.Count == 0)
            return null;

        var cheapestProduct = FindCheapestProduct(products, categoryCounts);

        return cheapestProduct;
    }

    private static Service.Product.Product? FindCheapestProduct(List<Service.Product.Product> products, List<Category> categoryCounts)
    {
        return products
            .Where(product => categoryCounts
                .Contains(product.Category))
            .MinBy(product => product.Price);
    }

    private static List<Category> FindCategoryCount(List<Service.Product.Product> products)
    {
        return products
            .GroupBy(product => product.Category)
            .Where(group => group.Count() >= MinCategoryCount)
            .Select(group => group.Key)
            .ToList();
    }
}