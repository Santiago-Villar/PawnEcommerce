using Service.Exception;
using Service.Promotion;
using Service.User;

namespace Service.Sale;

public class SaleService
{
    private readonly ISaleRepository _saleRepository;
    private readonly IPromotionService _promotionService;

    public SaleService(ISaleRepository saleRepository)
    {
        _saleRepository = saleRepository;
        _promotionService = new PromotionService();
    }

    public void Create(Sale sale)
    {
        try
        {
            var promotion = _promotionService.GetPromotion(sale.Products);
            sale.PromotionName = promotion.Name;

            var newPrice = promotion.GetDiscountPrice(sale.Products);
            sale.Price = newPrice;
            
            _saleRepository.Add(sale);
        }
        catch (ServiceException ex)
        {
            _saleRepository.Add(sale);
        }
    }
}