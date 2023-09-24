namespace Service.Filter.ConcreteFilter;
using Service.Product;
public class BrandFilter
{
    public bool Match(Product product, Brand brand)
    {
        return product.Brand.Equals(brand);
    }
}