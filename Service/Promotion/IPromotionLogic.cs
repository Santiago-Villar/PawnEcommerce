using System;
using Service.Product;
namespace Service.Promotion
{
	public interface IPromotionLogic
	{
		public IPromotionStrategy GetPromotion(List<IProduct> products);
	}
}

