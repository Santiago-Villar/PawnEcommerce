using Service.Filter.ConcreteFilter;

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
}