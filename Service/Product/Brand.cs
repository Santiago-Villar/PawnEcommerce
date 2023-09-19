namespace Service.Product
{
    public class Brand
    {
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (value.Equals(""))
                {
                    throw new ServiceException("Brand Name must not be empty");
                }
                _name = value;
            }

        }

        public Brand(string name)
        {
            this.Name = name;
        }
    }
}