namespace Service.Filter.ConcreteFilter;
using Service.Product;

public class CategoryFilter
{
    public bool Match(Product product, Category category)
    {
        return product.Category.Equals(category);
    }
}