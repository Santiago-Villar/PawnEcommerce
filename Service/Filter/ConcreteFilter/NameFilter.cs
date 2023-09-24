namespace Service.Filter.ConcreteFilter;
using Service.Product;

public class NameFilter : FilterTemplate
{
    public override bool Match(Product product, IFilterCriteria stringCriteria)
    {
        if (stringCriteria is StringFilterCriteria criteria)
        {
            return product.Name.Contains(criteria.Value, StringComparison.OrdinalIgnoreCase);
        }
        throw new ArgumentException("Invalid Criteria type. Expected StringCriteria.");
    }
}