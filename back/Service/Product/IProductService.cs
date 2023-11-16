using Service.Filter.ConcreteFilter;
using Service.User;
using Service.DTO.Product;

namespace Service.Product
{
    public interface IProductService
    {
        IProductRepository _productRepository { get; set; }
        IColorService _colorService { get; set; }
        public Product AddProduct(Product Product);
        public void DeleteProduct(int id);
        public Product Get(int id);
        public Product GetProductByName(string productName);
        public Product[] GetAllProducts(FilterQuery filter);
        public void UpdateProduct(Product product);


        public void IncreaseStock(int productId, int quantity);

        public void DecreaseStock(int productId, int quantity);

        public (Product[] UpdatedCart, List<Product> RemovedProducts) VerifyAndUpdateCart(Product[] cartProducts);

        public string GenerateRemovalNotification(List<Product> removedProducts);


        public Product UpdateProductUsingDTO(int id, ProductUpdateModel productDTO);
        public void Reset();
    }
}