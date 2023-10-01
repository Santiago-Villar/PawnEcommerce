using Service.Sale;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

namespace Repository
{
    public class SaleRepository : ISaleRepository
    {
        private readonly EcommerceContext _context;

        public SaleRepository(EcommerceContext context)
        {
            _context = context;
        }

        public void Add(Sale sale)
        {
            if (sale == null)
                throw new ArgumentNullException(nameof(sale));

            _context.Sales.Add(sale);
            _context.SaveChanges();
        }

        public List<Sale> GetUserSales(int userId)
        {
            if (userId <= 0)
                throw new ArgumentException("Invalid user ID", nameof(userId));

            return _context.Sales.Where(s => s.UserId == userId).ToList();
        }

        public List<Sale> GetAll()
        {
            return _context.Sales.ToList();
        }
    }
}


