using System.Collections.Generic;
using System.Linq;
using Service.Filter;
using Service.Filter.ConcreteFilter;
using Service.Product;

public  class CartUtils
{
    private static IProductService _productService;

    public CartUtils(IProductService productService)
    {
        _productService = productService;
    }

    public static Product[] VerifyAndUpdateCart(Product[] cartProducts)
    {
        List<Product> updatedCart = new List<Product>();

        // 1. Create a filter based on the product IDs in the cart.
        // Inside VerifyAndUpdateCart method...
        FilterQuery filter = new FilterQuery
        {
            ProductIds = new IdsFilterCriteria { Ids = cartProducts.Select(p => p.Id).ToList() }
        };

        // 2. Fetch the latest versions of products in the cart.
        Product[] latestProducts = _productService.GetAllProducts(filter);

        // 3. Loop through the cart products and check against the fetched products.
        foreach (var cartProduct in cartProducts)
        {
            var latestProduct = latestProducts.FirstOrDefault(p => p.Id == cartProduct.Id);

            if (latestProduct == null)
            {
                // Product not found in the database anymore. It could be deleted or filtered out.
                continue;
            }

            var productCountInCart = cartProducts.Count(p => p.Id == cartProduct.Id);

            if (latestProduct.IsStockAvailable(productCountInCart))
            {
                updatedCart.Add(latestProduct);  // Adding the latest product details to the cart.
            }
            else
            {
                // If not enough stock, then we'll remove all occurrences of this product from the cart.
                cartProducts = cartProducts.Where(p => p.Id != cartProduct.Id).ToArray();
            }
        }

        return updatedCart.ToArray();
    }

}

