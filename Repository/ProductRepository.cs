using Service.Product;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Service.Exception;
using Service.Filter.ConcreteFilter;


namespace Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly EcommerceContext _context;

        public ProductRepository(EcommerceContext context)
        {
            _context = context;
        }

        public int AddProduct(Product newProduct)
        {
            if (newProduct == null)
                throw new ArgumentNullException(nameof(newProduct));

            if(Exists(newProduct))
            {
                throw new ServiceException($"There is already a product with name {newProduct.Name}");
            }

            _context.Products.Add(newProduct);
            _context.SaveChanges();
            return newProduct.Id;
        }


        public void DeleteProduct(int productId)
        {
            var product = _context.Products.Find(productId);
            if (product == null)
                throw new ModelException($"Product with ID {productId} not found");

            _context.Products.Remove(product);
            _context.SaveChanges();
        }


        public Boolean Exists(int id)
        {
            return _context.Products.Any(p => p.Id == id);
        }
        public Boolean Exists(Product product)
        {
            return NameExists(product.Name);
        }


        public Product[] GetAllProducts(FilterQuery filter)
        {
            return _context.Products
                           .Include(p => p.Brand)
                           .Include(p => p.Category)
                           .Include(p => p.Colors)
                           .ToArray();
        }


        public Product GetProductByName(string productName)
        {
            return _context.Products
                           .Include(p => p.Brand)
                           .Include(p => p.Category)
                           .Include(p => p.Colors)
                           .FirstOrDefault(p => p.Name == productName);
        }

        public Product Get(int id)
        {
            return _context.Products
                           .Include(p => p.Brand)
                           .Include(p => p.Category)
                           .Include(p => p.Colors)
                           .FirstOrDefault(p => p.Id == id);
        }


        public void Reset()
        {
            foreach (var product in _context.Products)
            {
                _context.Products.Remove(product);
            }
            _context.SaveChanges();
        }



        public void UpdateProduct(Product newProductVersion)
        {

            _context.Products.Update(newProductVersion);
            _context.SaveChanges();
        }

        private Boolean NameExists(string name)
        {
            return _context.Products.Any(p => p.Name == name);
        }

    }

}
