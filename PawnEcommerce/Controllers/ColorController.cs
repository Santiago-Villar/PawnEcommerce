using System;
using Microsoft.AspNetCore.Mvc;
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
    }
}

