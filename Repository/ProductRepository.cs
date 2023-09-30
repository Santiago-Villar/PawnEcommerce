using Service.Product;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;


namespace Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly EcommerceContext _context;

        public ProductRepository(EcommerceContext context)
        {
            _context = context;
        }

        public void AddProduct(Product newProduct)
        {
            if (newProduct == null)
                throw new ArgumentNullException(nameof(newProduct));

            _context.Products.Add(newProduct);
            _context.SaveChanges();
        }


        public void DeleteProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            _context.Products.Remove(product);
            _context.SaveChanges();
        }


        public Boolean Exists(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            return _context.Products.Any(p => p.Id == product.Id);
        }


        public Product[] GetAllProducts()
        {
            return _context.Products
                           .Include(p => p.Brand)
                           .Include(p => p.Category)
                           .Include(p => p.Colors)
                           .ToArray();
        }


        public Product GetProductByName(string productName)
        {
            if (string.IsNullOrEmpty(productName))
                throw new ArgumentException("Product name should not be null or empty.", nameof(productName));

            return _context.Products
                           .Include(p => p.Brand)
                           .Include(p => p.Category)
                           .Include(p => p.Colors)
                           .FirstOrDefault(p => p.Name == productName);
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
            if (newProductVersion == null)
                throw new ArgumentNullException(nameof(newProductVersion));

            _context.Products.Update(newProductVersion);
            _context.SaveChanges();
        }

    }

}
