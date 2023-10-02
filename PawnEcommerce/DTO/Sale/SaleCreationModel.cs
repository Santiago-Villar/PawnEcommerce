using System.Diagnostics;
using PawnEcommerce.DTO.Product;
using Service.Sale;

namespace PawnEcommerce.DTO.Sale;

public class SaleCreationModel
{
    public int UserId { get; set; }
    public ProductDTO[] ProductDtos { get; set; }
    
    public Service.Sale.Sale ToEntity()
    {
        return new Service.Sale.Sale
        {
            UserId = UserId,
        };
    }

    public List<SaleProduct> CreateSaleProducts(Service.Sale.Sale sale)
    {
        var saleProducts = ProductDtos
            .Select(pDto =>
                new SaleProduct
                {
                    Product = pDto.ToEntity(),
                    SaleId = sale.Id, 
                    Sale = sale, 
                    ProductId = pDto.Id
                });

        return saleProducts.ToList();
    }
}