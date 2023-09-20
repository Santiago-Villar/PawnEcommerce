using Service.Exception;

namespace Service.Product
{
    public class Category
    {
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
    }
}