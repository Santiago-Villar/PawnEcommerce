using Service.Exception;
using Service.Filter;
using System.ComponentModel.DataAnnotations;

namespace Service.Product
{
    public class Category : ICategory
    {
        [Key]

        public int Id { get; set; }

        public Category(int id)
        {
            Id = id;
        }
        public Category() { }
        private string _name;
        public string Name { get => _name;
            set
            {
                if(value.Equals("")) { 
                    throw new ModelException("Category Name must not be empty");
                }
                _name = value;
            }
                
        }
        public ICollection<Product> Products { get; set; }

    }
}