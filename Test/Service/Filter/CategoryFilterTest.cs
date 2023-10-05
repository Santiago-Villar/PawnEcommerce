using Service.Filter;
using Service.Filter.ConcreteFilter;
using Service.Product;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace Test.Service.Filter;

[TestClass]
[ExcludeFromCodeCoverage]
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
        var category = new Category(1)
        {
            Name = "Tuber",
        };
        
        var product = new Product()
        {
            Category = category
        };
        
        var categoryFilter = new CategoryFilter();
        var match = categoryFilter.Match(product, new IdFilterCriteria(){ Value = category.Id });
        Assert.IsTrue(match);
    }
    
    [TestMethod]
    public void MatchCategory_DifferentCategory_Ok()
    {
        var category = new Category(2)
        {
            Name = "Vegetable",
        };
        
        var toCheckCategory = new Category(3)
        {
            Name = "Tuber",
        };
        
        var product = new Product()
        {
            Category = category
        };
        
        var categoryFilter = new CategoryFilter();
        var match = categoryFilter.Match(product, new IdFilterCriteria(){ Value = toCheckCategory.Id });
        Assert.IsFalse(match);
    }
}