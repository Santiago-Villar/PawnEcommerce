using System.Collections.Generic;
using System.Linq;
using Service.Filter;
using Service.Filter.ConcreteFilter;
using Service.Product;

public class CartUtils
{
    private static IProductService _productService;

    public CartUtils(IProductService productService)
    {
        _productService = productService;
    }

    public (Product[] UpdatedCart, List<Product> RemovedProducts) VerifyAndUpdateCart(Product[] cartProducts)
    {
        List<Product> updatedCart = new List<Product>();
        List<Product> removedProducts = new List<Product>();

        FilterQuery filter = new FilterQuery
        {
            ProductIds = new IdsFilterCriteria { Ids = cartProducts.Select(p => p.Id).ToList() }
        };

        Product[] latestProducts = _productService.GetAllProducts(filter);

        foreach (var cartProduct in cartProducts)
        {
            var latestProduct = latestProducts.FirstOrDefault(p => p.Id == cartProduct.Id);

            if (latestProduct == null)
            {
                removedProducts.Add(cartProduct);
                continue;
            }

            var productCountInCart = cartProducts.Count(p => p.Id == cartProduct.Id);

            if (latestProduct.IsStockAvailable(productCountInCart))
            {
                updatedCart.Add(latestProduct);
            }
            else
            {
                removedProducts.Add(cartProduct);
                cartProducts = cartProducts.Where(p => p.Id != cartProduct.Id).ToArray();
            }
        }

        return (updatedCart.ToArray(), removedProducts);
    }

    public string GenerateRemovalNotification(List<Product> removedProducts)
    {
        if (!removedProducts.Any())
            return string.Empty;

        var productNames = string.Join(", ", removedProducts.Select(p => p.Name));
        return $"The following products were removed from your cart due to insufficient stock or being no longer available: {productNames}.";
    }
}


