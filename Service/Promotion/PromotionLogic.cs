using System;
using Service.Product;
using Service.Exception;
namespace Service.Promotion
{
	public class PromotionLogic : IPromotionLogic
	{
		public PromotionLogic()
		{
			
		}

		public IPromotionStrategy GetPromotion(List<IProduct> products)
		{
			throw new ServiceException("Can not get promotion of empty list of products");
		}
	}
}

