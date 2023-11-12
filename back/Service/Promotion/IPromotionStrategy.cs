using Service.Product;

namespace Service.Promotion;

public interface IPromotionStrategy
{
    public string Name { get; init; }
    public string Description { get; init; }
    public double GetDiscount(List<Service.Product.Product> products);
    public double GetDiscountPrice(List<Service.Product.Product> products);
}