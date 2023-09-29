using System;
using Service;
namespace Service.Promotion
{
	public interface IPromotionService
	{
		public IPromotionStrategy GetPromotion(List<Product.Product> products);
	}
}

