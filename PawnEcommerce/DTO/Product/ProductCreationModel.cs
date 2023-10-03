using Service.Product;

namespace PawnEcommerce.DTO.Product;

public class ProductCreationModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
    public BrandDTO Brand { get; set; }
    public CategoryDTO Category { get; set; }
    public ColorDTO[] Colors { get; set; }

    public Service.Product.Product ToEntity()
    {
        return new Service.Product.Product
        {
            Name = Name,
            Description = Description,
            Price = Price,
            Brand = Brand.ToEntity(),
            Category = Category.ToEntity(),
            Colors = Colors.Select(color => color.ToEntity()).ToList()
        };
    }
}