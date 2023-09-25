using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service;
using Service.Product;
using Service.User;

namespace Service.Product
{
    public interface IProductRepository
    {
        void AddProduct(Product newProduct);
        Product GetProductByName(string productName, IUser owner);
        void UpdateProduct(Product newProductVersion, string previousVersionName);
        void DeleteProduct(Product product);
        Product GetAllProducts();
        Product[] GetProductsByUser(IUser user);
        Boolean Exists(Product product);
        void Reset();
    }
}