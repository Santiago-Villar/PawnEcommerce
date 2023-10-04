﻿using Service.Sale;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using Service.User;
using Service.Exception;

namespace Repository
{
    public class SaleRepository : ISaleRepository
    {
        private readonly EcommerceContext _context;

        public SaleRepository(EcommerceContext context)
        {
            _context = context;
        }

        public int Add(Sale sale)
        {
            if (sale == null)
                throw new ServiceException("can not read Sale");

            _context.Sales.Add(sale);
            _context.SaveChanges();
            return sale.Id;
        }

        public List<Sale> GetUserSales(int userId)
        {
            return _context.Sales
                           .Include(s => s.User)
                           .Include(s => s.Products)
                               .ThenInclude(sp => sp.Product)
                           .Where(s => s.UserId == userId)
                           .ToList();
        }

        public List<Sale> GetAll()
        {
            return _context.Sales
                           .Include(s => s.User)
                           .Include(s => s.Products)
                               .ThenInclude(sp => sp.Product)
                           .ToList();
        }

        public Sale Get(int id)
        {
            var sale = _context.Sales
                           .Include(s => s.User)
                           .Include(s => s.Products)
                               .ThenInclude(sp => sp.Product)
                           .FirstOrDefault(s => s.Id == id);

            if (sale == null)
            {
                throw new RepositoryException($"Sale with ID {id} not found");
            }

            return sale;
        }

        public void Update(Sale updateSale)
        {
            var existingSale = _context.Sales
                             .Include(s => s.Products)
                             .FirstOrDefault(s => s.Id == updateSale.Id);

            if (existingSale == null)
                throw new ServiceException($"Sale with ID {updateSale.Id} not found");

            _context.SaleProducts.RemoveRange(existingSale.Products);

            foreach (var sp in updateSale.Products)
            {
                _context.SaleProducts.Add(sp);
            }

            _context.Entry(existingSale).CurrentValues.SetValues(updateSale);
            _context.SaveChanges();
        }
    }
}


