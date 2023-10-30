using System;
using Service.User;
using Service.Product;
namespace Service.Sale
{
	public class Sale
	{
        public int Id { get; set; } 
        private ICollection<SaleProduct> products;
        public ICollection<SaleProduct> Products
        {
	        get => products;
	        set
	        {
		        products = value;
		        CalculateTotalPrice();
	        }
        }
        public int UserId { get; set; }
        public Service.User.User User { get; set; }

        public double? Price { get; set; }
        public string? PromotionName { get; set; }
        public DateTime Date { get; set; }
        public Sale()
		{
			Date = DateTime.Now;
        }
        private void CalculateTotalPrice()
        {
	        Price = Products.Select(sp => sp.Product).Sum(product => product.Price);
        }
	}
}

        