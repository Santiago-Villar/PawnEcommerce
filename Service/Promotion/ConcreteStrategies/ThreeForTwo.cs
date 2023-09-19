using Service.Product;

namespace Service.Promotion.ConcreteStrategies;

public class ThreeForTwo : IPromotionStrategy
{
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
        var categoryCounts = products
            .GroupBy(product => product.Category)
            .Where(group => group.Count() >= MinCategoryCount)
            .Select(group => group.Key)
            .ToList();

        if (categoryCounts.Count == 0)
            return null;

        var cheapestProduct = products
            .Where(product => categoryCounts
            .Contains(product.Category))
            .MinBy(product => product.Price);

        return cheapestProduct;
    }
}