using System;
namespace Service.Product
{
	public interface IBrandRepository
	{
        public List<Brand> GetAll();

        public Brand GetById(int id);

    }
}

