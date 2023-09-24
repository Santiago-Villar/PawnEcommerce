namespace Service.Filter;
using Service.Product;

public abstract class FilterTemplate
{
    public abstract bool Match(Product product, object filter);
}