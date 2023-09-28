using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Product;

namespace Service.Sale
{
    public class SaleProduct:ISaleProduct
    {
        public int SaleId { get; set; }
        public Sale Sale { get; set; } // Navigation property

        public int ProductId { get; set; }
        public Service.Product.Product Product { get; set; } // Navigation property
    }
}

