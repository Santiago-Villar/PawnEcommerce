using Service.User;

namespace Service.Product
{
    public interface IProductService
    {
        IProductRepository _productRepository { get; set; }
        public int AddProduct(Product Product);
        public void DeleteProduct(int id);
        public Product Get(int id);
        public Product GetProductByName(string productName);

        public Product[] GetAllProducts();
        public void UpdateProduct(Product mockProduct);
        public void Reset();
    }
}