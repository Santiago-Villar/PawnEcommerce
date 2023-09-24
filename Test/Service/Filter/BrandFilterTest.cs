using Service.Filter.ConcreteFilter;

namespace Test.Service.Filter;

[TestClass]
public class BrandFilterTest
{
    [TestMethod]
    public void CanCreateFilter_Ok()
    {
        var brandFilter = new BrandFilter();
        Assert.IsNotNull(brandFilter);
    }
}