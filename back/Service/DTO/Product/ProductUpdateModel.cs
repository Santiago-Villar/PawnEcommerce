using Service.Product;
using System.Diagnostics.CodeAnalysis;

namespace Service.DTO.Product;

[ExcludeFromCodeCoverage]
public class ProductUpdateModel
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int? Price { get; set; }
    public int? BrandId { get; set; }
    public int? CategoryId { get; set; }
    public int[]? Colors { get; set; }

    public bool? IsExcludedFromPromotions { get; set; }

}