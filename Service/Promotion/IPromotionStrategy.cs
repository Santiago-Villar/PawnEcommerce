using Service.Product;

namespace Service.Promotion;

public interface IPromotionStrategy
{
    public string Name { get; init; }
    public double GetDiscountPrice(List<IProduct> products);
}