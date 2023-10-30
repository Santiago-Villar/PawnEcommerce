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
        if(sale.Products != null) 
        {
            var promotion = _promotionService.GetPromotion(sale.Products.Select(sp => sp.Product).ToList());
            var newPrice = promotion.GetDiscountPrice(sale.Products.Select(sp => sp.Product).ToList());

            if (!newPrice.Equals(sale.Price))
            {
                sale.PromotionName = promotion.Name;
                sale.Price = newPrice;
            }
        }

        return _saleRepository.Add(sale);
    }

    public double GetDiscount(List<Product.Product> products)
    {
        var promotion = _promotionService.GetPromotion(products);
        var newPrice = promotion.GetDiscountPrice(products);

        return newPrice;
    }

    public void Update(Sale sale)
    {
        var promotion = _promotionService.GetPromotion(sale.Products.Select(sp => sp.Product).ToList());
        var newPrice = promotion.GetDiscountPrice(sale.Products.Select(sp => sp.Product).ToList());

        if (!newPrice.Equals(sale.Price))
        {
            sale.PromotionName = promotion.Name;
            sale.Price = newPrice;
        }

        _saleRepository.Update(sale);
    }

    public List<Sale> GetAll()
    {
        return _saleRepository.GetAll();
    }
    
    public Sale Get(int id)
    {
        return _saleRepository.Get(id);
    }

    public List<Sale> GetSalesByUserId(int userId)
    {
        return _saleRepository.GetSalesByUserId(userId);
    }


}