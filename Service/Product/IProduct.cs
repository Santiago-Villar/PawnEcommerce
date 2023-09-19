using Service.Product.Category;

namespace Service.Product;

public interface IProduct
{
    public string Name { get; set; }
    public int Price { get; set; }
    public ICategory Category { get; set; }
}