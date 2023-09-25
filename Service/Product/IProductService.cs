using Service.User;

namespace Service.Product
{
    public interface IProductService
    {
        IProductRepository _productRepository { get; set; }
        public void AddProduct(Product Product);
        public void DeleteProduct(Product mockProduct);
        public Product GetProductByName(string ProductName, IUser owner);
        public Product[] GetProductsByOwner(IUser User);
        public void UpdateProduct(Product mockProduct, string newName);
        public void Reset();
        public bool NewNameIsValid(string newName, Product Product);
    }
}