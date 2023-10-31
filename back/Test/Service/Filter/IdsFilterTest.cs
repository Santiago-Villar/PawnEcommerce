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


}
