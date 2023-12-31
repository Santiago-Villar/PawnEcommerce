﻿using Service.Exception;
using Service.Filter;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Service.Product
{
    public class Brand : IBrand
    {
        [Key]
        public int Id { get; set; }

        public Brand(int id)
        {
            Id = id;
        }

        public Brand() { }


        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (value.Equals(""))
                {
                    throw new RepositoryException("Brand Name must not be empty");
                }
                _name = value;
            }

        }
        [JsonIgnore]

        public ICollection<Product> Products { get; set; }


    }
}