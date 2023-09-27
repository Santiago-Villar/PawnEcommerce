using System;
using Service.Product;
namespace Service.Promotion
{
	public interface IPromotionService
	{
		public IPromotionStrategy GetPromotion(List<Service.Product.Product> products);
	}
}

