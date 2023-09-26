using Service.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Service.Exception;
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

        public void AddProduct(Product Product)
        {
            if(_productRepository.Exists(Product)) {
                throw new ServiceException("Product " + Product.Name + " already exists.");
            }
            else _productRepository.AddProduct(Product);
        }

        public void DeleteProduct(Product product)
        {
            if (_productRepository.Exists(product))
            {
                _productRepository.DeleteProduct(product);
            }
            else throw new ServiceException("Product " + product.Name + " does not exist.");
        }

        public Product GetProductByName(string productName)
        {
            if (string.IsNullOrEmpty(productName))
            {
                throw new ArgumentException("Product name cannot be null or empty.");
            }

            var product = _productRepository.GetProductByName(productName);
            if (product == null)
            {
                throw new ServiceException($"Product {productName} does not exist.");
            }

            return product;
        }
        public Product[] GetAllProducts()
        {
            return _productRepository.GetAllProducts();
        }
        public bool NewNameIsValid(string newName, Product Product)
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public void UpdateProduct(Product newProductVersion)
        {
            if(newProductVersion == null)
            {
                throw new ServiceException($"New version of a product cannot be null");
            }
            if (_productRepository.Exists(newProductVersion))
            {
                _productRepository.UpdateProduct(newProductVersion);
            }
            else { throw new ServiceException($"Product {newProductVersion.Name} does not exist."); }
        }

    }
}
