namespace Service.Product
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        private int _price;
        public int Price { get => _price;
            set {  _price = value; }
        }
        public Product(String name, String Description, int price) {
            this.Name = name;  
            this.Description = Description;
            this.Price=price
        }
    }
}