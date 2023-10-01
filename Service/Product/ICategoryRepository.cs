using System;
namespace Service.Product
{
    public interface ICategoryRepository
    {
        public List<Category> GetAll();

        public Category GetById(int id);

    }
}
