using System;
using Microsoft.AspNetCore.Mvc;
using Service.Product;

namespace PawnEcommerce.Controllers
{
    [Route("api/[controller]")]
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

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_brandService.Get(id));
        }

    }
}

