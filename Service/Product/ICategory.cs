namespace Service.Product;

public interface ICategory
{
    int? Id { get; set; }
    public string Name { get; set; }

    public ICollection<Product> Products { get; set; }
}