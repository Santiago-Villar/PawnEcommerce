﻿using Service.Exception;
using System.ComponentModel.DataAnnotations.Schema;

namespace Service.Product
{
    public class Product:IProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        private int _price;
        public int Price { get => _price;
            set {  
               if(value< 0) throw new ModelException("Price must be a positive integer.");
                _price = value; 
            }
        }

        [ForeignKey("Brand")]
        public string BrandName { get; set; } // Foreign Key to Brand
        public Brand Brand { get; set; }  // Navigation property

        [ForeignKey("Category")]
        public string CategoryName { get; set; } // Foreign Key to Category
        public Category Category { get; set; }  // Navigation property

        public ICollection<Color> Colors { get; set; }
        public Product()
        {
            Colors = new List<Color>();
        }

        public void AddColor(Color color)
        {
            if (color.Equals("")) throw new ModelException("Color must not be null");
            if (Colors.Contains(color)) return;
            Colors.Add(color);
        }
    }
}