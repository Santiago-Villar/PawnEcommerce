namespace Service.Product;

public interface IBrand
{
    public string Name { get; set; }

    public ICollection<Product> Products { get; set; }
}