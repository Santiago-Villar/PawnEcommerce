using Service.Product;

namespace PawnEcommerce.DTO.Product;

public class ProductDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
    public string BrandName { get; set; }
    public string CategoryName { get; set; }
    public string[] Colors { get; set; }

    public Service.Product.Product ToEntity()
    {
        return new Service.Product.Product
        {
            Name = Name,
            Description = Description,
            Price = Price,
            Brand = new Brand { Name = BrandName },
            Category = new Category { Name = CategoryName },
            Colors = Colors.Select(name => new Color { Name = name }).ToList()
        };
    }
}