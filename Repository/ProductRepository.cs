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
                throw new ServiceException(nameof(newProduct));

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
                throw new RepositoryException($"Product with ID {productId} not found");

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
            var query = _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.ProductColors)
                .ThenInclude(pc => pc.Color)
                .Where(p =>
                    (filter.CategoryId == null || p.CategoryId == filter.CategoryId.Value) &&
                    (filter.BrandId == null || p.BrandId == filter.BrandId.Value) &&
                    (filter.Name == null || p.Name.Contains(filter.Name.Value!))
                )
                .ToArray();

            return query;
        }



        public Product GetProductByName(string productName)
        {
            return _context.Products
                           .Include(p => p.Brand)
                           .Include(p => p.Category)
                           .Include(p => p.ProductColors)
                           .ThenInclude(pc => pc.Color)
                           .FirstOrDefault(p => p.Name == productName);
        }


        public Product Get(int id)
        {
            return _context.Products
                           .Include(p => p.Brand)
                           .Include(p => p.Category)
                           .Include(p => p.ProductColors)
                           .ThenInclude(pc => pc.Color)
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



        public void UpdateProduct(Product updatedProduct)
        {
            if (updatedProduct == null)
                throw new ModelException(nameof(updatedProduct));

            var existingProduct = _context.Products
                                         .Include(p => p.ProductColors)
                                         .FirstOrDefault(p => p.Id == updatedProduct.Id);

            if (existingProduct == null)
                throw new RepositoryException($"Product with ID {updatedProduct.Id} not found");

            // Remove existing color associations
            _context.ProductColors.RemoveRange(existingProduct.ProductColors);

            // Add updated color associations
            foreach (var color in updatedProduct.Colors)
            {
                _context.ProductColors.Add(new ProductColor { ProductId = updatedProduct.Id, ColorId = color.Id });
            }

            _context.Entry(existingProduct).CurrentValues.SetValues(updatedProduct);
            _context.SaveChanges();
        }

        private Boolean NameExists(string name)
        {
            return _context.Products.Any(p => p.Name == name);
        }

    }

}
