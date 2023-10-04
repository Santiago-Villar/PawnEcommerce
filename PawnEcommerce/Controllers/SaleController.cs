using Microsoft.AspNetCore.Mvc;
using PawnEcommerce.DTO.Product;
using PawnEcommerce.DTO.Sale;
using PawnEcommerce.Middlewares;
using Service.Product;
using Service.Sale;

namespace PawnEcommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ExceptionMiddleware]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;
        private readonly IProductService _productService;


        public SaleController(ISaleService saleService, IProductService productService)
        {
            _saleService = saleService;
            _productService = productService;
        }
        [Authorization("User")]
        [HttpPost]
        public IActionResult Create([FromBody] SaleCreationModel newSale)
        {
            var sale = newSale.ToEntity();
            sale.Id = _saleService.Create(sale);
            sale.Products = newSale.CreateSaleProducts(sale, _productService);
            _saleService.Update(sale);
            
            return Ok();
        }
        [Authorization("Admin")]
        [HttpGet]
        public IActionResult GetAll()
        {
            var sales = _saleService.GetAll();
            return Ok(sales);
        }
        [Authorization("Admin")]
        [HttpGet("{id:int}")]
        public IActionResult Get([FromRoute] int id)
        {
            var sales = _saleService.Get(id);
            return Ok(sales);
        }
        
        [HttpPost("Discount")]
        public IActionResult GetDiscount([FromBody] List<int> ids)
        {
            var newPrice = _saleService.GetDiscount(ids.Select(id => _productService.Get(id)).ToList());
            var saleDiscountDto = new SaleDiscountDTO { discountPrice = newPrice };
            return Ok(saleDiscountDto);
        }

    }
}