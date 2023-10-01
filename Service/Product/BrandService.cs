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
            try
            {
                var brand = _brandRepository.GetById(id) ?? throw new ModelException($"Brand with ID {id} not found.");
                return brand;
            }
            catch (RepositoryException ex)
            {
                throw new ModelException($"Error retrieving brand with ID {id}. Message: {ex.Message}");
            }
        }
    }
}

