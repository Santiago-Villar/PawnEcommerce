using System.Diagnostics;
using PawnEcommerce.DTO.Product;
using Service.Sale;

namespace PawnEcommerce.DTO.Sale;

public class SaleDTO
{
    public int UserId { get; set; }
    public ProductDTO[] ProductDtos { get; set; }
    
    public Service.Sale.Sale ToEntity()
    {
        return new Service.Sale.Sale
        {
            UserId = UserId,
            Products = ProductDtos.Select(pDto => 
                new SaleProduct { Product = pDto.ToEntity() }).ToList()
        };
    }
}