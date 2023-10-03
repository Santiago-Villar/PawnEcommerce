using System;
using System.Collections.Generic;
using System.Linq;
using Service.Product;
using Microsoft.EntityFrameworkCore;
using Service.Exception;

namespace Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly EcommerceContext _context;

        public CategoryRepository(EcommerceContext context)
        {
            _context = context;
        }

        public List<Category> GetAll()
        {
            return _context.Categories.ToList();
        }

        public Category GetById(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);

            if (category == null)
            {
                throw new ModelException($"No category found with ID {id}");
            }

            return category;
        }
    }
}

