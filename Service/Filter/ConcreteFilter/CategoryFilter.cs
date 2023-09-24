namespace Service.Filter.ConcreteFilter;
using Service.Product;

public class CategoryFilter : FilterTemplate
{
    public override bool Match(Product product, IFilterCriteria category)
    {
        return product.Category.Equals(category);
    }
}