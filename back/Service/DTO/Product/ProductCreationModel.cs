using Service.Product;
using System.Diagnostics.CodeAnalysis;

namespace Service.DTO.Product;

[ExcludeFromCodeCoverage]
public class ProductCreationModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
    public int BrandId { get; set; }
    public int CategoryId { get; set; }
    public int[] Colors { get; set; }

    public int Stock { get; set; }

    public bool IsExcludedFromPromotions { get; set; }

    public Service.Product.Product ToEntity(IBrandService _brandService, ICategoryService _categoryService, IColorService _colorService)
    {
        return new Service.Product.Product
        {
            Name = Name,
            Description = Description,
            Price = Price,
            BrandId = BrandId,
            CategoryId = CategoryId,
            Brand = _brandService.Get(BrandId),
            Category = _categoryService.Get(CategoryId),
            ProductColors = Colors.Distinct().Select(c => new ProductColor { Color = _colorService.Get(c) }).ToList(),
            Stock = Stock,
            IsExcludedFromPromotions = IsExcludedFromPromotions
        };
    }

}