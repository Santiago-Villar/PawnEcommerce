using IRepository;
using Service.Product;
using Service.User;

namespace Logic
{
    public interface IProductService
    {
        IProductRepository _productRepository { get; set; }
        public Product AddProduct(Product Product);
        public Product DeleteProduct(Product mockProduct);
        public Product GetProductByName(string ProductName, User owner);
        public Product[] GetProductsByOwner(User User);
        public Product UpdateProduct(Product mockProduct, string newName);
        public void Reset();
        public bool NewNameIsValid(string newName, Product Product);
    }
}