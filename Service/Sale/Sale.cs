using System;
using Service.User;
using Service.Product;
namespace Service.Sale
{
	public class Sale
	{
        public int Id { get; set; } // Primary Key

        public string UserEmail { get; set; } // Assuming you use Email as primary key in User
        public Service.User.User User { get; set; }
        public ICollection<SaleProduct> Products { get; set; }

        public DateTime Date { get; set; }
        public Sale()
		{
			Date = DateTime.Now;
        }
	}
}

