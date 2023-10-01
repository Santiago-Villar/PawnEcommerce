using System;
namespace Service.Product
{
	public class CategoryService : ICategoryService
	{
        public ICategoryRepository _categoryRepository { get; set; }

        public CategoryService(ICategoryRepository repo)
        {
            _categoryRepository = repo;
        }

        public List<Category> GetAll()
        {
            return _categoryRepository.GetAll();
        }

        public Category Get(int id)
        {
            return _categoryRepository.GetById(id);
        }
    }

}

