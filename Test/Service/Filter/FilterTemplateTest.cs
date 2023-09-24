using Service.Filter;
using Service.Filter.ConcreteFilter;
using Service.Product;

namespace Test.Service.Filter;

[TestClass]
public class FilterTemplateTest
{
    [TestMethod]
    public void CanFilter_NameFilter_Ok()
    {
        FilterTemplate nameFilter = new NameFilter();
        const string filter = "Bread";
        
        var potato = new Product() { Name = "Potato" };
        var pizza = new Product() { Name = "Pizza" };
        var bread = new Product() { Name = "Bread" };
        var integralBread = new Product() { Name = "IntegralBread" };
        
        
        var products = new List<Product>()
        {
            potato,
            pizza,
            bread,
            integralBread
        };

        var filteredProducts = nameFilter.Filter(products, filter);

        var toCheckList = new List<Product>()
        {
            bread,
            integralBread
        };
        
        CollectionAssert.AreEqual(filteredProducts, toCheckList);
    }
}