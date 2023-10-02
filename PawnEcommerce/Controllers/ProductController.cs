using Microsoft.AspNetCore.Mvc;
using Service.Product;
using PawnEcommerce.DTO.Product;

namespace PawnEcommerce.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        
        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _productService.GetAllProducts();
            return Ok(products);
        }
        
        [HttpGet("{id:int}")]
        public IActionResult Get([FromRoute] int id)
        {
            var products = _productService.Get(id);
            return Ok(products);
        }
        
        [HttpPost]
        public IActionResult Create([FromBody] ProductCreationModel newProduct)
        {
            _productService.AddProduct(newProduct.ToEntity());
            return Ok();
        }
                
        [HttpPut]
        public IActionResult Update([FromBody] ProductCreationModel updateProduct)
        {
            _productService.UpdateProduct(updateProduct.ToEntity());
            return Ok();
        }
        
        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            _productService.DeleteProduct(id);
            return Ok();
        }
    }
}