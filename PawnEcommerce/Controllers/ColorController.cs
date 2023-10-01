using System;
using Microsoft.AspNetCore.Mvc;
using Service.Exception;
using Service.Product;

namespace PawnEcommerce.Controllers
{
    [Route("api/[controller]")]
    public class ColorController : ControllerBase
    {
        private IColorService _colorService { get; set; }

        public ColorController(IColorService service)
        {
            _colorService = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_colorService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
           return Ok(_colorService.Get(id));
        }
    }
}

