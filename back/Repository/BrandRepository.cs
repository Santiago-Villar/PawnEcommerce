﻿using Service.Product;
using System.Collections.Generic;
using System.Linq;
using System;
using Service.Exception;

namespace Repository
{

    public class BrandRepository : IBrandRepository
    {
        private readonly EcommerceContext _context;

        public BrandRepository(EcommerceContext context)
        {
            _context = context;
        }

        public List<Brand> GetAll()
        {
            return _context.Brands.ToList();
        }

        public Brand GetById(int id)
        {
            var brand = _context.Brands.FirstOrDefault(b => b.Id == id);

            if (brand == null)
                throw new RepositoryException($"No Brand found with the ID: {id}");

            return brand;
        }
    }
}

