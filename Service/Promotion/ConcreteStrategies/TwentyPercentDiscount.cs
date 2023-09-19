using System.Net;
using Service.Product;

namespace Service.Promotion.ConcreteStrategies;

public class TwentyPercentDiscount : IPromotionStrategy
{
    private const double TwentyPercentConverter = 0.8;
    public double GetDiscountPrice(List<IProduct> products)
    {
        if (products.Count < 2)
            return products.Sum(product => product.Price);
        
        var maxPrice = products.Max(product => product.Price);
        var discountPrice = products.Sum(product => product.Price) - maxPrice + maxPrice * TwentyPercentConverter;
    
        return discountPrice;
    }
}