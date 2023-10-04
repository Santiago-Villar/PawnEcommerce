using System;
using Service.Exception;

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
            var brand = _brandRepository.GetById(id) ?? throw new RepositoryException($"Brand with ID {id} not found.");
            return brand;
        }
    }
}

