using Service.User;

namespace Service.Sale;

public interface ISaleService
{
    public void Create(Sale sale);
    public List<Sale> GetAll();
    public List<Sale> Get(int id);
}