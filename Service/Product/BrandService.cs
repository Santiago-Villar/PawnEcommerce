using System;
namespace Service.Product
{
	public class BrandService : IBrandService
	{
        public IBrandRepository _brandRepository { get; set; }

        public BrandService(IBrandRepository repo)
        {
            _brandRepository = repo;
        }

        public List<Brand> GetAll()
        {
            return _brandRepository.GetAll();
        }

        public Brand Get(int id)
        {
            return _brandRepository.GetById(id);
        }
    }
}

