using Service.Exception;
using Service.Filter;
using System.ComponentModel.DataAnnotations;

namespace Service.Product
{
    public class Category : IFilterCriteria,ICategory
    {
        private string _name;
        [Key]
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