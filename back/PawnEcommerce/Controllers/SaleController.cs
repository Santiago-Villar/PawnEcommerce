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

                var cartProducts = newSale.ProductDtosId.Select(id => _productService.Get(id)).ToArray();

                var (updatedCart, removedProducts) = _productService.VerifyAndUpdateCart(cartProducts);

                if (removedProducts.Any())//Si no hay stock para todos los productos
                {
                    var removalNotification = _productService.GenerateRemovalNotification(removedProducts);
                    return StatusCode(StatusCodes.Status409Conflict, new { updatedCart, Message = removalNotification });
                }

                if (updatedCart.Any())//Si hay para todos y el carrito NO est� vac�o
                {
                    var sale = newSale.ToEntity();
                    sale.UserId = userId.Value;

                    sale.Products = newSale.CreateSaleProducts(sale, updatedCart, _productService);

                    sale.Id = _saleService.Create(sale);

                    _saleService.Update(sale);

                    int[] emptyCart = new int[] { }; 

                    return Ok(new {emptyCart, Message = "Sale created successfully" });
                }
                else //Se hizo una Sale con el carrito vac�o
                {
                    int[] emptyCart = new int[] { };
                    return BadRequest(new {emptyCart, Message = "There are no products available for sale in your cart." });
                }
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
        [HttpGet("purchase-history")] 
        public IActionResult GetUserPurchaseHistory() 
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var sessionService = scope.ServiceProvider.GetRequiredService<ISessionService>();

                var userId = sessionService.ExtractUserIdFromToken(Request.Headers["Authorization"].ToString().Split(' ')[1]);
                if (!userId.HasValue)
                    return Unauthorized("Invalid token.");

                var sales = _saleService.GetSalesByUserId(userId.Value);
                return Ok(sales);
            }
        }


    }
}