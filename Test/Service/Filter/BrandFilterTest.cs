using Service.Filter.ConcreteFilter;
using Service.Product;

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
    
    [TestMethod]
    public void MatchBrand_Ok()
    {
        var brand = new Brand()
        {
            Name = "Tuber",
        };
        
        var product = new Product()
        {
            Brand = brand
        };
        
        var brandFilter = new BrandFilter();
        var match = brandFilter.Match(product, brand);
        Assert.IsTrue(match);
    }
}