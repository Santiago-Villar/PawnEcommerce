using Service.Product;

namespace Service.Promotion.ConcreteStrategies;

public class ThreeForTwo : IPromotionStrategy
{
    public string Name { get; init; } = "3x2";
    public string Description { get; init; } = "Al tener 3 productos de la misma categoría, el producto de menor valor es gratis.";

    private const int MinCategoryCount = 3;

    public double GetDiscountPrice(List<Service.Product.Product> products)
    {
        var totalPrice = products.Sum(product => product.Price);
        var discount = GetDiscount(products);
        
        return totalPrice - discount;
    }

    public double GetDiscount(List<Service.Product.Product> products)
    {
        var eligibleProducts = products.Where(p => !p.IsExcludedFromPromotions).ToList();
        var cheapestProduct = FindCheapestProductInCommonCategories(eligibleProducts);
        return cheapestProduct?.Price ?? 0;
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