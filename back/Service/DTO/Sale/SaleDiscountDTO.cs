using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Service.DTO.Product;
using Service.Product;
using Service.Sale;

namespace Service.DTO.Sale;

[ExcludeFromCodeCoverage]
public class SaleDiscountDTO
{
    public double discountPrice { get; set; }

}