using Service.Exception;

namespace Service.Filter.ConcreteFilter;
using Service.Product;
public class BrandFilter : FilterTemplate
{
    private IBrandService _brandService;
    public override bool Match(Product product, IFilterCriteria idFilterCriteria)
    {
        if (idFilterCriteria is IdFilterCriteria idFilter)
        {
            return product.Brand.Id.Equals(idFilter.Value);
        }
        throw new ModelException("Invalid Criteria type. Expected IdFilterCriteria.");
    }
}