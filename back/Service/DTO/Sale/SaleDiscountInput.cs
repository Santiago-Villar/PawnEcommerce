using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Service.DTO.Product;
using Service.Product;
using Service.Sale;

namespace Service.DTO.Sale;

[ExcludeFromCodeCoverage]
public class SaleDiscountInput
{
    public int[] ProductIds { get; set; }
    public string? PaymentMethod { get; set; }
}