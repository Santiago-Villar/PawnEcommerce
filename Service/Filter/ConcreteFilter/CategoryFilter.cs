using Service.Exception;

namespace Service.Filter.ConcreteFilter;
using Service.Product;

public class CategoryFilter : FilterTemplate
{
    public override bool Match(Product product, IFilterCriteria idFilterCriteria)
    {
        if (idFilterCriteria is IdFilterCriteria idFilter)
        {
            return product.Category.Id.Equals(idFilter.Value);
        }
        throw new ModelException("Invalid Criteria type. Expected IdFilterCriteria.");
    }
}