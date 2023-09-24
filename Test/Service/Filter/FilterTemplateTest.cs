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
    
    [TestMethod]
    public void CanFilter_BrandFilter_Ok()
    {
        FilterTemplate brandFilter = new BrandFilter();
        var filterBrand = new Brand(){ Name = "Vegetable" };
        var otherBrand = new Brand(){ Name = "Tuber" };

        var potato = new Product() { Brand = filterBrand };
        var pizza = new Product() { Brand = filterBrand };
        var bread = new Product() { Brand = otherBrand };
        var integralBread = new Product() { Brand = otherBrand };
        
        var products = new List<Product>()
        {
            potato,
            pizza,
            bread,
            integralBread
        };

        var filteredProducts = brandFilter.Filter(products, filterBrand);

        var toCheckList = new List<Product>()
        {
            potato,
            pizza
        };
        
        CollectionAssert.AreEqual(filteredProducts, toCheckList);
    }
    
    [TestMethod]
    public void CanFilter_CategoryFilter_Ok()
    {
        FilterTemplate categoryFilter = new CategoryFilter();
        var filterBrand = new Category(){ Name = "Vegetable" };
        var otherBrand = new Category(){ Name = "Tuber" };

        var potato = new Product() { Category = filterBrand };
        var pizza = new Product() { Category =  otherBrand };
        var bread = new Product() { Category = otherBrand };
        var integralBread = new Product() { Category = filterBrand };
        
        var products = new List<Product>()
        {
            potato,
            pizza,
            bread,
            integralBread
        };

        var filteredProducts = categoryFilter.Filter(products, filterBrand);

        var toCheckList = new List<Product>()
        {
            potato,
            integralBread
        };
        
        CollectionAssert.AreEqual(filteredProducts, toCheckList);
    }
}