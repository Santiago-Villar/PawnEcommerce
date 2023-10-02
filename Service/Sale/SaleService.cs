using Service.Exception;
using Service.Promotion;
using Service.User;
using Service.Product;
namespace Service.Sale;

public class SaleService : ISaleService
{
    private readonly ISaleRepository _saleRepository;
    private readonly IPromotionService _promotionService;

    public SaleService(ISaleRepository saleRepository)
    {
        _saleRepository = saleRepository;
        _promotionService = new PromotionService();
    }

    public int Create(Sale sale)
    {
        try
        {
            var (promotion, newPrice) = GetDiscount(sale.Products.Select(sp => sp.Product).ToList());
            
            if (!newPrice.Equals(sale.Price))
            {
                sale.PromotionName = promotion.Name;
                sale.Price = newPrice;
            }
            
            return _saleRepository.Add(sale);
        }
        catch (ServiceException ex) 
        {
            throw new ServiceException(ex.Message);
        }
    }

    public (IPromotionStrategy, double) GetDiscount(List<Product.Product> products)
    {
        var promotion = _promotionService.GetPromotion(products);
        var newPrice = promotion.GetDiscountPrice(products);

        return (promotion, newPrice);
    }
    
    public List<Sale> GetAll()
    {
        return _saleRepository.GetAll();
    }
    
    public List<Sale> GetByUser(int userId)
    {
        return _saleRepository.GetUserSales(userId);
    }

    public Sale Get(int id)
    {
        return _saleRepository.Get(id);
    }

}