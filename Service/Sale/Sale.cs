using System;
using Service.User;
using Service.Product;
namespace Service.Sale
{
	public class Sale
	{
        public IUser User { get; set; }
        private List<IProduct> products;
        public List<IProduct> Products
        {
	        get => products;
	        init
	        {
		        products = value;
		        CalculateTotalPrice();
	        }
        }
        public int Price { get; set; }
        public DateTime Date { get; set; }
        public string PromotionName { get; set; }
        
        public Sale()
		{
			Date = DateTime.Now;
        }

        private void CalculateTotalPrice()
        {
	        Price = Products.Sum(product => product.Price);
        }
	}
}

