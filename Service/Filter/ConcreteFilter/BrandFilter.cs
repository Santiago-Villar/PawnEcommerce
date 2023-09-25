namespace Service.Filter.ConcreteFilter;
using Service.Product;
public class BrandFilter : FilterTemplate
{
    public override bool Match(Product product, IFilterCriteria brand)
    {
        return product.Brand.Equals(brand);
    }
}