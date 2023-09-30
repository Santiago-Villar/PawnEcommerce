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
        
        [HttpGet("{name}")]
        public IActionResult Get([FromRoute] string name)
        {
            var products = _productService.GetProductByName(name);
            return Ok(products);
        }
        
        [HttpPost]
        public IActionResult Create([FromBody] ProductDTO newProduct)
        {
            _productService.AddProduct(newProduct.ToEntity());
            return Ok();
        }
                
        [HttpPut]
        public IActionResult Update([FromBody] ProductDTO updateProduct)
        {
            _productService.UpdateProduct(updateProduct.ToEntity());
            return Ok();
        }
        
        [HttpDelete]
        public IActionResult Delete([FromBody] ProductDTO deleteProduct)
        {
            _productService.DeleteProduct(deleteProduct.ToEntity());
            return Ok();
        }
    }
}