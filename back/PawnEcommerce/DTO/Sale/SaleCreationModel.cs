using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using PawnEcommerce.DTO.Product;
using Service.Product;
using Service.Sale;

namespace PawnEcommerce.DTO.Sale;

[ExcludeFromCodeCoverage]
public class SaleCreationModel
{
    public int[] ProductDtosId { get; set; }
    
    public Service.Sale.Sale ToEntity()
    {
        return new Service.Sale.Sale();
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