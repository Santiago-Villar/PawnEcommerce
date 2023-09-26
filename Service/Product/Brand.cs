using Service.Exception;
using Service.Filter;

namespace Service.Product
{
    public class Brand : IFilterCriteria,IBrand
    {
        private string _name;
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


    }
}