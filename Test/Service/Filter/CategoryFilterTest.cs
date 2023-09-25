using Service.Filter.ConcreteFilter;
using Service.Product;

namespace Test.Service.Filter;

[TestClass]
public class CategoryFilterTest
{
    [TestMethod]
    public void CanCreateFilter_Ok()
    {
        var categoryFilter = new CategoryFilter();
        Assert.IsNotNull(categoryFilter);
    }
    
    [TestMethod]
    public void MatchCategory_Ok()
    {
        var category = new Category()
        {
            Name = "Tuber",
        };
        
        var product = new Product()
        {
            Category = category
        };
        
        var categoryFilter = new CategoryFilter();
        var match = categoryFilter.Match(product, category);
        Assert.IsTrue(match);
    }
    
    [TestMethod]
    public void MatchCategory_DifferentCategory_Ok()
    {
        var category = new Category()
        {
            Name = "Vegetable",
        };
        
        var toCheckCategory = new Category()
        {
            Name = "Tuber",
        };
        
        var product = new Product()
        {
            Category = category
        };
        
        var categoryFilter = new CategoryFilter();
        var match = categoryFilter.Match(product, toCheckCategory);
        Assert.IsFalse(match);
    }
}