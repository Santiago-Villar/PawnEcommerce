using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service;
using Service.Product;
using Service.User;

namespace IRepository
{
    public interface IProductRepository
    {
        Product AddProduct(Product newProduct);
        Product GetProductByName(string productName, User owner);
        Product UpdateProduct(Product newProductVersion, string previousVersionName);
        Product DeleteProduct(Product product);
        Product GetAllProducts();
        Product[] GetProductsByUser(User user);
        Boolean Exists(Product product);
        void Reset();
    }
}