using Microsoft.AspNetCore.Mvc;
using Service.DTO.Product;
using Service.DTO.Sale;
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

                var userId = sessionService.GetCurrentUser().Id;

                var cartProducts = newSale.ProductIds.Select(id => _productService.Get(id)).ToArray();

                var (updatedCart, removedProducts) = _productService.VerifyAndUpdateCart(cartProducts);

                if (removedProducts.Any())//Si no hay stock para todos los productos
                {
                    var removalNotification = _productService.GenerateRemovalNotification(removedProducts);
                    return StatusCode(StatusCodes.Status409Conflict, new { updatedCart, Message = removalNotification });
                }

                if (updatedCart.Any())//Si hay para todos y el carrito NO est� vac�o
                {
                    var sale = newSale.ToEntity();
                    sale.UserId = userId;

                    sale.PaymentMethod = newSale.PaymentMethod;

                    sale.Id = _saleService.Create(sale);

                    sale.Products = newSale.CreateSaleProducts(sale, updatedCart, _productService);

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


        [HttpGet("{id:int}")]
        public IActionResult Get([FromRoute] int id)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var sessionService = scope.ServiceProvider.GetRequiredService<ISessionService>();

                var user = sessionService.GetCurrentUser();  

                if (user == null)
                    return Unauthorized("User was not found");

                var userId = user.Id;

                var sale = _saleService.Get(id);

                if (sale.UserId != userId && !user.Roles.Contains(Service.User.Role.RoleType.Admin))
                    return Unauthorized("User has no access");

                return Ok(sale);
            }
        }
        
        [HttpPost("Discount")]
        public IActionResult GetDiscount([FromBody] SaleDiscountInput discountInfo)
        {
            var products = discountInfo.ProductIds.Select(id => _productService.Get(id)).ToList();
            var promotion = _saleService.GetPromotion(products);
            
            var result = new SaleDiscountDTO()
            {
                PromotionName = promotion.Name,
                PromotionDescription = promotion.Description,
                PaymentMethod = discountInfo.PaymentMethod,
                TotalPrice = _saleService.GetTotalPrice(products),
                PromotionDiscount = promotion.GetDiscount(products),
                PaymentMethodDiscount = _saleService.GetPaymentMethodDiscount(products, discountInfo.PaymentMethod),
                FinalPrice = _saleService.GetFinalPrice(products, discountInfo.PaymentMethod),
            };

            return Ok(result);
        }

        [Authorization("User")]
        [HttpGet("History")] 
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