using Microsoft.VisualBasic.CompilerServices;
using Service.Product;

namespace Service.Promotion;


public class PromotionSelector
{
    private readonly List<IPromotionStrategy> _promotions;

    public PromotionSelector()
    {
        var promotionCollection = new PromotionCollection();
        _promotions = promotionCollection.GetPromotions();
    }
    
    public IPromotionStrategy? GetBestPromotion(List<Service.Product.Product> products)
    {
        return _promotions.MinBy(promo => promo.GetDiscountPrice(products));
    }
}