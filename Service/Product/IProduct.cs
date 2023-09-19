using Service.Product.Brand;
using Service.Product.Category;
using Service.Product.Color;

namespace Service.Product;

public interface IProduct
{
    public string Name { get; set; }
    public int Price { get; set; }
    public ICategory Category { get; set; }
    public List<IColor> Colors { get; set; }
    public IBrand Brand { get; set; }
}