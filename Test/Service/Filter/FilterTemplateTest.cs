using Service.Filter;
using Service.Filter.ConcreteFilter;
using Service.Product;

namespace Test.Service.Filter;

[TestClass]
public class FilterTemplateTest
{
    private static readonly Brand FilterBrand = new Brand(1){ Name = "Vegetable" };
    private static readonly Brand OtherBrand = new Brand(2){ Name = "Tuber" };
    private static readonly Category FilterCategory = new Category(1){ Name = "Vegetable" };
    private static readonly Category OtherCategory = new Category(2){ Name = "Tuber" };
    
    private static readonly Product Potato = new Product()
    {
        Name = "Potato",
        Brand = FilterBrand,
        Category = FilterCategory
    };
    
    private static readonly Product Pizza = new Product()
    {
        Name = "Pizza",
        Brand = FilterBrand,
        Category = OtherCategory
    };
    
    private static readonly Product Bread = new Product()
    {
        Name = "Bread",
        Brand = OtherBrand,
        Category = OtherCategory
    };
    
    private static readonly Product IntegralBread = new Product()
    {
        Name = "IntegralBread",
        Brand = OtherBrand,
        Category = FilterCategory
    };
    
    private readonly List<Product> Products = new List<Product>()
    {
        Potato,
        Pizza,
        Bread,
        IntegralBread
    };
    
    [TestMethod]
    public void CanFilter_NameFilter_Ok()
    {
        FilterTemplate nameFilter = new NameFilter();
        
        const string filter = "Bread";
        var filterCriteria = new StringFilterCriteria()
        {
            Value = filter
        };

        var filteredProducts = nameFilter.Filter(Products, filterCriteria);

        var toCheckList = new List<Product>()
        {
            Bread,
           IntegralBread
        };
        
        CollectionAssert.AreEqual(filteredProducts, toCheckList);
    }
    
    [TestMethod]
    public void CanFilter_BrandFilter_Ok()
    {
        FilterTemplate brandFilter = new BrandFilter();
        var filteredProducts = brandFilter.Filter(Products, FilterBrand);

        var toCheckList = new List<Product>()
        {
            Potato,
            Pizza
        };
        
        CollectionAssert.AreEqual(filteredProducts, toCheckList);
    }
    
    [TestMethod]
    public void CanFilter_CategoryFilter_Ok()
    {
        FilterTemplate categoryFilter = new CategoryFilter();
        var filteredProducts = categoryFilter.Filter(Products, FilterCategory);

        var toCheckList = new List<Product>()
        {
            Potato,
            IntegralBread
        };
        
        CollectionAssert.AreEqual(filteredProducts, toCheckList);
    }
}