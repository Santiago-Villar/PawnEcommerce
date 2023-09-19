namespace Service.Product
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public Product(String name, String Description, int price) {
            this.Name = name;  
            this.Description = Description;
            this.Price=price
        }
    }
}