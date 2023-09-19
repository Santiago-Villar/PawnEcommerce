using Service.Product;
using Service.Product.Brand;
using Service.Product.Category;

namespace Service.Promotion.ConcreteStrategies;

public class ThreeForOne : IPromotionStrategy
{
    private const int MinBrandCount = 3;
    private const int MaxFreeItems = 2;
    public double GetDiscountPrice(List<IProduct> products)
    {
        var totalPrice = products.Sum(product => product.Price);
        var cheapestProduct = FindCheapestProductsInCommonBrands(products);
        var discountPrice = cheapestProduct?.Sum(product => product.Price) ?? 0;
        
        return totalPrice - discountPrice;
    }
    
    private static List<IProduct>? FindCheapestProductsInCommonBrands(List<IProduct> products)
    {
        var brandCounts = FindBrandCount(products);

        if (brandCounts.Count == 0)
            return null;    

        var cheapestProducts = FindCheapestProducts(products, brandCounts);

        return cheapestProducts;
    }

    private static List<IProduct> FindCheapestProducts(List<IProduct> products, List<IBrand> categoryCounts)
    {
        return products
            .Where(product => categoryCounts.Contains(product.Brand))
            .OrderBy(product => product.Price)
            .Take(MaxFreeItems)
            .ToList();
    }

    private static List<IBrand> FindBrandCount(List<IProduct> products)
    {
        return products
            .GroupBy(product => product.Brand)
            .Where(group => group.Count() >= MinBrandCount)
            .Select(group => group.Key)
            .ToList();
    }
}