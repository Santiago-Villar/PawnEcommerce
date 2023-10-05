namespace Service.Filter;
using Service.Product;

public abstract class FilterTemplate
{
    public List<Product> Filter(List<Product> products, IFilterCriteria filter)
    {
        return products.Where(product => Match(product, filter)).ToList();
    }
    
    public abstract bool Match(Product product, IFilterCriteria filter);
}