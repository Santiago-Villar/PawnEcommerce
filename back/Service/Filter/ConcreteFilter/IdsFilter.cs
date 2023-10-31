

namespace Service.Filter.ConcreteFilter
{
    using Service.Exception;
    using Service.Product;

    public class IdsFilter : FilterTemplate
    {
        public override bool Match(Product product, IFilterCriteria filterCriteria)
        {
            if (filterCriteria is IdsFilterCriteria criteria)
            {
                return criteria.Ids.Contains(product.Id);
            }
            throw new ModelException("Invalid Criteria type. Expected IdsFilterCriteria.");
        }
    }
}