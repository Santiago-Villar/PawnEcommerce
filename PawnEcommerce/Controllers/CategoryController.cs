﻿using System;
using Microsoft.AspNetCore.Mvc;
using Service.Exception;
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

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_categoryService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var category = _categoryService.Get(id);
                return Ok(category);
            }
            catch (ModelException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}