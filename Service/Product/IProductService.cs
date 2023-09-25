using Service.User;

namespace Service.Product
{
    public interface IProductService
    {
        IProductRepository _productRepository { get; set; }
        public Product AddProduct(Product Product);
        public Product DeleteProduct(Product mockProduct);
        public Product GetProductByName(string ProductName, IUser owner);
        public Product[] GetProductsByOwner(IUser User);
        public Product UpdateProduct(Product mockProduct, string newName);
        public void Reset();
        public bool NewNameIsValid(string newName, Product Product);
    }
}