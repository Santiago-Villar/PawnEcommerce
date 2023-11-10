using Service.Exception;
using Service.Filter;
using Service.Filter.ConcreteFilter;
using Service.Product;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;

namespace Test.Service.Filter;

[TestClass]
[ExcludeFromCodeCoverage]
public class PriceFilterTest
{
    [TestMethod]
    public void CanCreatePriceFilter_Ok()
    {
        var priceFilter = new PriceFilter();
        Assert.IsNotNull(priceFilter);
    }

    [TestMethod]
    public void Match_PriceMatched_Ok()
    {
        var filterCriteria = new PriceFilterCriteria(50, 100);
        var product = new Product()
        {
            Price = 75
        };

        var priceFilter = new PriceFilter();
        var match = priceFilter.Match(product, filterCriteria);
        Assert.IsTrue(match);
    }

    [TestMethod]
    public void Match_PriceNotMatched_Ok()
    {
        var filterCriteria = new PriceFilterCriteria(50, 100);
        var product = new Product()
        {
            Price = 101
        };

        var priceFilter = new PriceFilter();
        var match = priceFilter.Match(product, filterCriteria);
        Assert.IsFalse(match);
    }

}

