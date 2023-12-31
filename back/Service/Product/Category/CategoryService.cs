﻿using System;
using Service.Exception;

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
            var category = _categoryRepository.GetById(id) ?? throw new RepositoryException($"Category with ID {id} not found.");
            return category;
        }
    }

}

