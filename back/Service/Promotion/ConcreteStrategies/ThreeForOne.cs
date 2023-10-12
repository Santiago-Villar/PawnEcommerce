using Service.Product;

namespace Service.Promotion.ConcreteStrategies;

public class ThreeForOne : IPromotionStrategy
{
    public string Name { get; init; } = "Three For One";

    private const int MinBrandCount = 3;
    private const int MaxFreeItems = 2;
    public double GetDiscountPrice(List<Service.Product.Product> products)
    {
        var totalPrice = products.Sum(product => product.Price);
        var cheapestProduct = FindCheapestProductsInCommonBrands(products);
        var discountPrice = cheapestProduct?.Sum(product => product.Price) ?? 0;
        
        return totalPrice - discountPrice;
    }
    
    private static List<Service.Product.Product>? FindCheapestProductsInCommonBrands(List<Service.Product.Product> products)
    {
        var brandCounts = FindBrandCount(products);

        if (brandCounts.Count == 0)
            return null;    

        var cheapestProducts = FindCheapestProducts(products, brandCounts);

        return cheapestProducts;
    }

    private static List<Service.Product.Product> FindCheapestProducts(List<Service.Product.Product> products, List<Brand> categoryCounts)
    {
        return products
            .Where(product => categoryCounts.Contains(product.Brand))
            .OrderBy(product => product.Price)
            .Take(MaxFreeItems)
            .ToList();
    }

    private static List<Brand> FindBrandCount(List<Service.Product.Product> products)
    {
        return products
            .GroupBy(product => product.Brand)
            .Where(group => group.Count() >= MinBrandCount)
            .Select(group => group.Key)
            .ToList();
    }
}