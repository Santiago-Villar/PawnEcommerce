using System.Net.Sockets;
using Service.User;

namespace Service.Sale;

public interface ISaleRepository
{
    public void Add(Sale sale);
    public List<Sale> GetUserSales(IUser user);
    public List<Sale> GetAll();
}