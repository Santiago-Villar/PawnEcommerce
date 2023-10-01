using Service.User;

namespace Service.Sale;

public interface ISaleService
{
    public int Create(Sale sale);
    public List<Sale> GetAll();
    public List<Sale> GetByUser(int userId);
    public Sale Get(int id);
}