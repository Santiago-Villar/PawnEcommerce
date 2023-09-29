using System;
using Service.User;
using Service.Product;
namespace Service.Sale
{
	public class Sale
	{
        public int Id { get; set; } 

        public string UserEmail { get; set; } 
        public Service.User.User User { get; set; }
        public ICollection<SaleProduct> Products { get; set; }

        public DateTime Date { get; set; }
        public Sale()
		{
			Date = DateTime.Now;
        }
	}
}

