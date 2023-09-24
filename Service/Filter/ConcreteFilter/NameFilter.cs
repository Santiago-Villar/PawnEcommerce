namespace Service.Filter.ConcreteFilter;
using Service.Product;
public class NameFilter : FilterTemplate
{
    public override bool Match(Product product, object name)
    {
        return product.Name.Contains(name as string, StringComparison.OrdinalIgnoreCase);
    }
}