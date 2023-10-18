using Service.Sale;
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

        public List<Sale> GetAll()
        {
            return _context.Sales
                        .Include(s => s.Products)
                        .ThenInclude(sp => sp.Product)
                        .ThenInclude(p => p.Brand)  
                        .Include(s => s.Products)
                        .ThenInclude(sp => sp.Product)
                        .ThenInclude(p => p.Category)  
                        .Include(s => s.Products)
                        .ThenInclude(sp => sp.Product)
                        .ThenInclude(p => p.ProductColors)
                        .ThenInclude(pc => pc.Color)
                        .ToList();
        }

        public Sale Get(int id)
        {
            var sale = _context.Sales
                         .Include(s => s.Products)
                         .ThenInclude(sp => sp.Product)
                         .ThenInclude(p => p.ProductColors)
                         .ThenInclude(pc => pc.Color)
                         .FirstOrDefault(s => s.Id == id);

            if (sale == null)
            {
                throw new RepositoryException($"Sale with ID {id} not found");
            }

            return sale;
        }

        public List<Sale> GetSalesByUserId(int userId)
        {
            return _context.Sales
                        .Where(s => s.UserId == userId)
                        .Include(s => s.Products)
                        .ThenInclude(sp => sp.Product)
                        .ToList();
        }


        public void Update(Sale updateSale)
        {
            var existingSale = _context.Sales
                             .Include(s => s.Products)
                             .FirstOrDefault(s => s.Id == updateSale.Id);

            if (existingSale == null)
                throw new ServiceException($"Sale with ID {updateSale.Id} not found");

            foreach (var sp in updateSale.Products)
            {
                _context.SaleProducts.Add(sp);
            }

            _context.Entry(existingSale).CurrentValues.SetValues(updateSale);
            _context.SaveChanges();
        }
    }
}


