using Service.Exception;
using Service.Filter;
using System.ComponentModel.DataAnnotations;

namespace Service.Product
{
    public class Brand : IFilterCriteria,IBrand
    {
        public int? Id { get; set; }

        public Brand(int id)
        {
            Id = id;
        }
        public Brand() { }


        private string _name;
        [Key]
        public string Name
        {
            get => _name;
            set
            {
                if (value.Equals(""))
                {
                    throw new ModelException("Brand Name must not be empty");
                }
                _name = value;
            }

        }
        public ICollection<Product> Products { get; set; }


    }
}