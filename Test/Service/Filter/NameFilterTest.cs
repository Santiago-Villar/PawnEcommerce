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
        var match = nameFilter.Match(product);
        Assert.IsTrue(match);
    }
}