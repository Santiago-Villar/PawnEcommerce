using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using PawnEcommerce.Middlewares;
using Service.Exception;
using Service.Product;

namespace PawnEcommerce.Controllers
{
    [Route("api/[controller]")]
    [ExceptionMiddleware]
    public class BrandController : ControllerBase
    {
        private IBrandService _brandService { get; set; }

        public BrandController(IBrandService service)
		{
            _brandService = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_brandService.GetAll());
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            return Ok(_brandService.Get(id));
        }

    }
}

