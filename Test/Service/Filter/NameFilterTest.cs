using Service.Filter.ConcreteFilter;
using Service.Product;

namespace Test.Service.Filter;

[TestClass]
public class NameFilterTest
{
    [TestMethod]
    public void CanCreateFilter_Ok()
    {
        var nameFilter = new NameFilter();
        Assert.IsNotNull(nameFilter);
    }
    
    [TestMethod]
    public void Match_EqualName_Ok()
    {
        const string name = "Potato";
        var product = new Product()
        {
            Name = name
        };
        
        var nameFilter = new NameFilter();
        var match = nameFilter.Match(product, name);
        Assert.IsTrue(match);
    }
    
    [TestMethod]
    public void Match_IncludesName_Ok()
    {
        const string productName = "Potato";
        const string filterQuery = "Pot";
        var product = new Product()
        {
            Name = productName
        };
        
        var nameFilter = new NameFilter();
        var match = nameFilter.Match(product, filterQuery);
        Assert.IsTrue(match);
    }
}