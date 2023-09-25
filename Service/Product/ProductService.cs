using Service.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Product
{
    public class ProductService : IProductService
    {
        public IProductRepository _productRepository { get; set; }
        public ProductService(IProductRepository repo) 
        {
            _productRepository = repo;
        }

        public Product AddProduct(Product Product)
        {
            return _productRepository.AddProduct(Product);
        }

        public Product DeleteProduct(Product mockProduct)
        {
            throw new NotImplementedException();
        }

        public Product GetProductByName(string ProductName, IUser owner)
        {
            throw new NotImplementedException();
        }

        public Product[] GetProductsByOwner(IUser User)
        {
            throw new NotImplementedException();
        }

        public bool NewNameIsValid(string newName, Product Product)
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public Product UpdateProduct(Product mockProduct, string newName)
        {
            throw new NotImplementedException();
        }
    }
}
