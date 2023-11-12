using System.Net;
using Service.Product;

namespace Service.Promotion.ConcreteStrategies;

public class TwentyPercentDiscount : IPromotionStrategy
{
    public string Name { get; init; } = "20% OFF";
    public string Description { get; init; } = "Al tener 2 productos cualesquiera sean, obtienes 20% de descuento en el producto de mayor valor.";

    private const double TwentyPercentConverter = 0.2;
    

    public double GetDiscountPrice(List<Service.Product.Product> products)
    {
        var eligibleProducts = products.Where(p => !p.IsExcludedFromPromotions).ToList();
        var totalPrice = eligibleProducts.Sum(product => product.Price);

        if (eligibleProducts.Count < 2)
            return totalPrice;
        
        var discount = GetDiscount(eligibleProducts);
        return totalPrice - discount;
    }

    public double GetDiscount(List<Service.Product.Product> products)
    {
        var promoProduct = products.Max(product => product.Price);
        return promoProduct * TwentyPercentConverter;
    }

}