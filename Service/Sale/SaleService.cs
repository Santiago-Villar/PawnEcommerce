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
            var newPrice = promotion.GetDiscountPrice(sale.Products);
            
            if (!newPrice.Equals(sale.Price))
            {
                sale.PromotionName = promotion.Name;
                sale.Price = newPrice;
            }
            
            _saleRepository.Add(sale);
        }
        catch (ServiceException ex) 
        {
            throw new ServiceException(ex.Message);
        }
    }
    
    public List<Sale> GetAll()
    {
        return _saleRepository.GetAll();
    }
    
    public List<Sale> Get(IUser user)
    {
        return _saleRepository.GetUserSales(user);
    }
}