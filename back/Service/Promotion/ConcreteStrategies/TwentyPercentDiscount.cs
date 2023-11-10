using System.Net;
using Service.Product;

namespace Service.Promotion.ConcreteStrategies;

public class TwentyPercentDiscount : IPromotionStrategy
{
    public string Name { get; init; } = "Twenty Percent Discount";

    private const double TwentyPercentConverter = 0.8;
    

    public double GetDiscountPrice(List<Service.Product.Product> products)
    {
        var eligibleProducts = products.Where(p => !p.IsExcludedFromPromotions).ToList();

        if (eligibleProducts.Count < 2)
            return eligibleProducts.Sum(product => product.Price);

        var totalPrice = eligibleProducts.Sum(product => product.Price);
        var mostExpensiveProductPrice = eligibleProducts.Max(product => product.Price);
        var reducedPrice = mostExpensiveProductPrice * TwentyPercentConverter;

        return totalPrice - mostExpensiveProductPrice + reducedPrice;
    }

}