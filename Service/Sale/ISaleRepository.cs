using System.Net.Sockets;
using Service.User;

namespace Service.Sale;

public interface ISaleRepository
{
    public int Add(Sale sale);
    public List<Sale> GetUserSales(int id);
    public List<Sale> GetAll();
    public void Update(Sale sale);
    public Sale Get(int id);
}