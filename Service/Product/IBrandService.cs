using System;
namespace Service.Product
{
	public interface IBrandService
	{
        IBrandRepository _brandRepository { get; set; }

        public List<Brand> GetAll();
    }
}

