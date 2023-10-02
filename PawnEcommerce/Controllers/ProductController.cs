using Microsoft.AspNetCore.Mvc;
using Service.Product;
using PawnEcommerce.DTO.Product;
using Service.Filter;
using Service.Filter.ConcreteFilter;

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
        public IActionResult GetAll([FromQuery] string? name, [FromQuery] int? categoryId, [FromQuery] int? brandId)
        {
            var filter = new FilterQuery()
            {
                Name = new StringFilterCriteria() { Value = name },
                BrandId = new IdFilterCriteria() { Value = brandId },
                CategoryId = new IdFilterCriteria() { Value = categoryId }
            };
            
            var products = _productService.GetAllProducts(filter);
            return Ok(products);
        }
        
        [HttpGet("{id:int}")]
        public IActionResult Get([FromRoute] int id)
        {
            var product = _productService.Get(id);
            return Ok(product);
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