using Service.Promotion;
using Service.User;

namespace Service.Sale;

public interface ISaleService
{
    public int Create(Sale sale);
    public List<Sale> GetAll();
    public double GetDiscount(List<Product.Product> products);
    public void Update(Sale sale);
    public List<Sale> GetByUser(int userId);
    public Sale Get(int id);
}