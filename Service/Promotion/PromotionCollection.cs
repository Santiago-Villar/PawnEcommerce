using Service.Promotion.ConcreteStrategies;

namespace Service.Promotion;

public class PromotionCollection
{
    private List<IPromotionStrategy>? _promotions;
    public List<IPromotionStrategy> GetPromotions()
    {
        return _promotions ??= new List<IPromotionStrategy>()
        {
            new TotalLook(),
            new ThreeForOne(),
            new ThreeForTwo(),
            new TwentyPercentDiscount()
        };
    }
}