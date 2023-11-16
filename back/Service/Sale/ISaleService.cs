using Service.Promotion;
using Service.User;

namespace Service.Sale;

public interface ISaleService
{
    public int Create(Sale sale);
    public List<Sale> GetAll();
    public double GetFinalPrice(List<Product.Product> products, string paymentMethod);
    public double GetPaymentMethodDiscount(List<Product.Product> products, string paymentMethod);
    public IPromotionStrategy GetPromotion(List<Product.Product> products);
    public double GetTotalPrice(List<Product.Product> products);

    public void Update(Sale sale);
    public Sale Get(int id);

    public List<Sale> GetSalesByUserId(int userId);
}