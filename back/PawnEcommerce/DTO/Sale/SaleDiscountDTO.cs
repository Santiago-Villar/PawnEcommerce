using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using PawnEcommerce.DTO.Product;
using Service.Product;
using Service.Sale;

namespace PawnEcommerce.DTO.Sale;

[ExcludeFromCodeCoverage]
public class SaleDiscountDTO
{
    public double discountPrice { get; set; }

}