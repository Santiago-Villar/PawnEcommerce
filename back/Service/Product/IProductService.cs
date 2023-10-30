using Service.Filter.ConcreteFilter;
using Service.User;
using Service.DTO.Product;

namespace Service.Product
{
    public interface IProductService
    {
        IProductRepository _productRepository { get; set; }
        IColorService _colorService { get; set; }
        public int AddProduct(Product Product);
        public void DeleteProduct(int id);
        public Product Get(int id);
        public Product GetProductByName(string productName);
        public Product[] GetAllProducts(FilterQuery filter);
        public void UpdateProduct(Product product);
        public Product UpdateProductUsingDTO(int id, ProductUpdateModel productDTO);
        public void Reset();
    }
}