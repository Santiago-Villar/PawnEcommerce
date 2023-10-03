using System.Diagnostics;
using PawnEcommerce.DTO.Product;
using Service.Product;
using Service.Sale;

namespace PawnEcommerce.DTO.Sale;

public class SaleCreationModel
{
    public int UserId { get; set; }
    public int[] ProductDtosId { get; set; }
    
    public Service.Sale.Sale ToEntity()
    {
        return new Service.Sale.Sale
        {
            UserId = UserId,
        };
    }

    public List<SaleProduct> CreateSaleProducts(Service.Sale.Sale sale, IProductService productService)
    {
        var productDtos = ProductDtosId.Select(id => productService.Get(id)).ToList();

        var saleProducts = productDtos
            .Select(prod =>
                new SaleProduct
                {
                    Product = prod,
                    SaleId = sale.Id, 
                    Sale = sale, 
                    ProductId = prod.Id
                });

        return saleProducts.ToList();
    }
}