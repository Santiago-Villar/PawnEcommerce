using System;
using Service.Product;

namespace PawnEcommerce.Controllers
{
	public class BrandController
	{
        private IBrandService _brandService { get; set; }

        public BrandController(IBrandService service)
		{
            _brandService = service;
        }
	}
}

