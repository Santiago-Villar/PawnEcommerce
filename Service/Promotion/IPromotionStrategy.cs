using Service.Product;

namespace Service.Promotion;

public interface IPromotionStrategy
{
    public double GetDiscountPrice(List<IProduct> products);
}