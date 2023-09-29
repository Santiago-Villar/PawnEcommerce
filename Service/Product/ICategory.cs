namespace Service.Product;

public interface ICategory
{
    public string Name { get; set; }

    public ICollection<Product> Products { get; set; }
}