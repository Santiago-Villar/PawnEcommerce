﻿using Service.Product;
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
            throw new NotImplementedException();
        }

        public Product GetProductByName(string productName)
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public void UpdateProduct(Product newProductVersion)
        {
            throw new NotImplementedException();
        }
    }
    
}
