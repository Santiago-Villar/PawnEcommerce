namespace Service.Filter;
using Service.Product;

public abstract class FilterTemplate
{
    public List<Product> Filter(List<Product> products, object filter)
    {
        return products.Where(product => Match(product, filter)).ToList();
    }
    
    public abstract bool Match(Product product, object filter);
}