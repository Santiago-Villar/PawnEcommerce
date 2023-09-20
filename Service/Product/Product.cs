using Service.Exception;

namespace Service.Product
{
    public class Product
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

        public Category Category { get; set; }
        public Brand Brand { get; set; }

        public List<Color> Colors { get; set; }

        public void AddColor(Color color)
        {
            if (color.Equals("")) throw new ModelException("Color must not be null");
            if (Colors.Contains(color)) return;
            Colors.Add(color);
        }
    }
}