namespace Service.Filter.ConcreteFilter;
using Service.Product;
public class NameFilter
{
    public bool Match(Product product, string name)
    {
        return product.Name == name;
    }
}