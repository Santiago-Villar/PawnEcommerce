namespace Service.Sale;

public class SaleService
{
    private readonly ISaleRepository _saleRepository;
    
    public SaleService(ISaleRepository saleRepository)
    {
        _saleRepository = saleRepository;
    }
}