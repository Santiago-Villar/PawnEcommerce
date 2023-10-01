using Microsoft.AspNetCore.Mvc;
using PawnEcommerce.DTO;
using PawnEcommerce.DTO.Sale;
using PawnEcommerce.Middlewares;
using Service.Sale;
using Service.User;

namespace PawnEcommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;

        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }
    }
}