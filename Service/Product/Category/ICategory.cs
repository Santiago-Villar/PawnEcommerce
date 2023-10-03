namespace Service.Product;

public interface ICategory
{


    public int Id { get; set; }

    public string Name { get; set; }

    public ICollection<Product> Products { get; set; }
}