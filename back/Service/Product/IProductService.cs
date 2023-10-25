using Service.Filter.ConcreteFilter;
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
        public Product[] GetAllProducts(FilterQuery filter);
        public void UpdateProduct(Product product);
        public void UpdateProductUsingDTO(int id, ProductCreationModel productDTO);
        public void Reset();
    }
}