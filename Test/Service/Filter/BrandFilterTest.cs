using Moq;
using Service.Filter;
using Service.Filter.ConcreteFilter;
using Service.Product;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace Test.Service.Filter;

[TestClass]
[ExcludeFromCodeCoverage]
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
        var brand = new Brand(4)
        {
            Name = "Tuber",
        };
        
        var product = new Product()
        {
            Brand = brand
        };
        
        var brandFilter = new BrandFilter();
        var match = brandFilter.Match(product, new IdFilterCriteria(){ Value = brand.Id });
        Assert.IsTrue(match);
    }
    
    [TestMethod]
    public void MatchBrand_DifferentBrand_Ok()
    {
        var brand = new Brand(2)
        {
            Name = "Tuber",
        };
        
        var toCheckBrand = new Brand(1)
        {
            Name = "Vegetable",
        };
        
        var product = new Product()
        {
            Brand = brand
        };
        
        var brandFilter = new BrandFilter();
        var match = brandFilter.Match(product, new IdFilterCriteria(){ Value = toCheckBrand.Id });
        Assert.IsFalse(match);
    }
}