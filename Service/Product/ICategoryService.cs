using System;
namespace Service.Product
{
	public interface ICategoryService
	{
        ICategoryRepository _categoryRepository { get; set; }

        public List<Category> GetAll();
    }
}

