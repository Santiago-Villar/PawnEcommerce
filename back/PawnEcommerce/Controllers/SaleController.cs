using Microsoft.AspNetCore.Mvc;
using PawnEcommerce.DTO.Product;
using PawnEcommerce.DTO.Sale;
using PawnEcommerce.Middlewares;
using Service.Product;
using Service.Sale;
using Service.Session;

namespace PawnEcommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ExceptionMiddleware]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;
        private readonly IProductService _productService;
        private readonly IServiceProvider _serviceProvider;


        public SaleController(ISaleService saleService, IProductService productService, IServiceProvider serviceProvider)
        {
            _saleService = saleService;
            _productService = productService;
            _serviceProvider = serviceProvider;
        }

        [Authorization("User")]
        [HttpPost]
        public IActionResult Create([FromBody] SaleCreationModel newSale)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var sessionService = scope.ServiceProvider.GetRequiredService<ISessionService>();

                var userId = sessionService.ExtractUserIdFromToken(Request.Headers["Authorization"].ToString().Split(' ')[1]);
                if (!userId.HasValue)
                    return Unauthorized("Invalid token.");

                var sale = newSale.ToEntity();
                sale.UserId = userId.Value;

                sale.Id = _saleService.Create(sale);
                sale.Products = newSale.CreateSaleProducts(sale, _productService);
                _saleService.Update(sale);

                return Ok();
            }
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

        [Authorization("User")]
        [HttpGet("user/{userId:int}")]
        public IActionResult GetSalesByUserId([FromRoute] int userId)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var sessionService = scope.ServiceProvider.GetRequiredService<ISessionService>();

                var sales = _saleService.GetSalesByUserId(userId);
                return Ok(sales);
            }
        }

    }
}