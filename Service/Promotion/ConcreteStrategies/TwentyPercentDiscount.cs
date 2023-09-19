using System.Net;
using Service.Product;

namespace Service.Promotion.ConcreteStrategies;

public class TwentyPercentDiscount : IPromotionStrategy
{
    private const double TwentyPercentConverter = 0.8;
    
    public double GetDiscountPrice(List<IProduct> products)
    {
        var totalPrice = products.Sum(product => product.Price);

        if (products.Count < 2)
            return totalPrice;
        
        var toReducePrice = products.Max(product => product.Price);
        var reducedPrice = toReducePrice * TwentyPercentConverter;
        
        var discountPrice = totalPrice - toReducePrice + reducedPrice;
    
        return discountPrice;
    }
}