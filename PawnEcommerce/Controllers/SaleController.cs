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
        
        [HttpPost]
        public IActionResult Create([FromBody] SaleDTO newSale)
        {
            _saleService.Create(newSale.ToEntity());
            return Ok();
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] string nameId, [FromQuery] string categoryId, [FromQuery] string brandId)
        {
            var sales = _saleService.GetAll();
            return Ok(sales);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get([FromRoute] int id)
        {
            var sales = _saleService.Get(id);
            return Ok(sales);
        }
    }
}