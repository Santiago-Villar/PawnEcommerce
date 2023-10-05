using System;
using Microsoft.AspNetCore.Mvc;
using PawnEcommerce.Middlewares;
using Service.Exception;
using Service.Product;

namespace PawnEcommerce.Controllers
{
    [Route("api/[controller]")]
    [ExceptionMiddleware]
    public class CategoryController : ControllerBase
    {
        private ICategoryService _categoryService { get; set; }

        public CategoryController(ICategoryService service)
        {
            _categoryService = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_categoryService.GetAll());
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            return Ok(_categoryService.Get(id));
        }
    }
}