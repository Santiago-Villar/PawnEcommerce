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

        public List<Brand> GetAll()
        {
            return _brandService.GetAll();
        }

        public Brand Get(int id)
        {
            return _brandService.Get(id);
        }

    }
}

