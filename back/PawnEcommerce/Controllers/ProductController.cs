using Microsoft.AspNetCore.Mvc;
using Service.Product;
using PawnEcommerce.DTO.Product;
using PawnEcommerce.Middlewares;
using Service.Filter;
using Service.Filter.ConcreteFilter;

namespace PawnEcommerce.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [ExceptionMiddleware]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;
        private readonly IColorService _colorService;


        public ProductController(IProductService productService, ICategoryService categoryService, IBrandService brandService, IColorService colorService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _brandService = brandService;
            _colorService = colorService;
        }
        
        [HttpGet]
        public IActionResult GetAll([FromQuery] string? name, [FromQuery] int? categoryId, [FromQuery] int? brandId)
        {
            var filter = new FilterQuery();

            if(name != null)
                filter.Name = new StringFilterCriteria() { Value = name };

            if(categoryId != null)
                filter.CategoryId = new IdFilterCriteria() { Value = categoryId };
            
            if(brandId != null)
                filter.BrandId = new IdFilterCriteria() { Value = brandId };

            var products = _productService.GetAllProducts(filter);
            return Ok(products);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get([FromRoute] int id)
        {
            var product = _productService.Get(id);
            return Ok(product);
        }
        [Authorization("Admin")]
        [HttpPost]
        public IActionResult Create([FromBody] ProductCreationModel newProduct)
        {
            var product = newProduct.ToEntity(_brandService, _categoryService, _colorService);

            _productService.AddProduct(product);
            return Ok();
        }
        [Authorization("Admin")]
        [HttpPut("{id:int}")]
        public IActionResult Update([FromRoute] int id, [FromBody] ProductCreationModel updateProduct)
        {
            var product = updateProduct.ToEntity(_brandService, _categoryService, _colorService);
            product.Id = id;

            _productService.UpdateProduct(product);
            return Ok();
        }
        [Authorization("Admin")]
        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            _productService.DeleteProduct(id);
            return Ok();
        }
    }
}