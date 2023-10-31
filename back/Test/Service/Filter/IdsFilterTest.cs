using Service.Exception;
using Service.Filter;
using Service.Filter.ConcreteFilter;
using Service.Product;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace Test.Service.Filter;

[TestClass]
[ExcludeFromCodeCoverage]
public class IdsFilterTest
{
    [TestMethod]
    public void CanCreateIdsFilter_Ok()
    {
        var idsFilter = new IdsFilter();
        Assert.IsNotNull(idsFilter);
    }

    [TestMethod]
    public void Match_IdMatched_Ok()
    {
        const int productId = 1;
        var filterCriteria = new IdsFilterCriteria()
        {
            Ids = new List<int> { 1, 2, 3 }
        };

        var product = new Product()
        {
            Id = productId
        };

        var idsFilter = new IdsFilter();
        var match = idsFilter.Match(product, filterCriteria);
        Assert.IsTrue(match);
    }

    [TestMethod]
    public void Match_IdNotMatched_Ok()
    {
        const int productId = 5;
        var filterCriteria = new IdsFilterCriteria()
        {
            Ids = new List<int> { 1, 2, 3 }
        };

        var product = new Product()
        {
            Id = productId
        };

        var idsFilter = new IdsFilter();
        var match = idsFilter.Match(product, filterCriteria);
        Assert.IsFalse(match);
    }


}
