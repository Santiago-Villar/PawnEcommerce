using System;
using Service.Product;
using Service.Exception;
namespace Service.Promotion
{
	public class PromotionLogic : IPromotionLogic
	{
		private PromotionSelector Selector { get; set; }

		public PromotionLogic()
		{
			Selector = new PromotionSelector();
		}

		public IPromotionStrategy GetPromotion(List<IProduct> products)
		{
			if (products.Count == 0)
			{
				throw new ServiceException("Can not get promotion of empty list of products");
			}

			return Selector.GetBestPromotion(products);

		}
	}
}

