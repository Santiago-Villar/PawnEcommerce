using Service.Promotion;
using Service.User;

namespace Service.Sale;

public interface ISaleService
{
    public int Create(Sale sale);
    public List<Sale> GetAll();
    public (IPromotionStrategy, double) GetDiscount(List<Product.Product> products);
    public List<Sale> GetByUser(int userId);
    public Sale Get(int id);
}