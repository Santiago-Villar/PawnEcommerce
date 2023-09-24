using Service.Filter.ConcreteFilter;

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
}