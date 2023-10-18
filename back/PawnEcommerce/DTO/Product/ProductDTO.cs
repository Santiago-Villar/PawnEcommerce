using Service.Product;
using System.Diagnostics.CodeAnalysis;

namespace PawnEcommerce.DTO.Product;

[ExcludeFromCodeCoverage]
public class ProductDTO
{
    public int Id { get; set; }
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
            Id = Id,
            Name = Name,
            Description = Description,
            Price = Price,
            Brand = Brand.ToEntity(),
            Category = Category.ToEntity(),
            ProductColors = Colors.Select(color => new ProductColor { Color = color.ToEntity() }).ToList()
        };
    }

}