using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Service.DTO.Product;
using Service.Product;
using Service.Sale;

namespace Service.DTO.Sale;

[ExcludeFromCodeCoverage]
public class SaleDiscountDTO
{
    public string PromotionName { get; set; } = "";
    public string PromotionDescription {get; set;} = "";
    public string? PaymentMethod { get; set; }
    public double TotalPrice { get; set; }
    public double PromotionDiscount { get; set; }
    public double PaymentMethodDiscount { get; set; }
    public double FinalPrice { get; set; }
}