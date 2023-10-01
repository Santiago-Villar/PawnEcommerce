using System;
using Microsoft.AspNetCore.Mvc;
using Service.Product;

namespace PawnEcommerce.Controllers
{
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private ICategoryService _categoryService { get; set; }

        public CategoryController(ICategoryService service)
        {
            _categoryService = service;
        }
    }
}