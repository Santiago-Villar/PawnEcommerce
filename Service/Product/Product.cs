namespace Service.Product
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Product(String name) {
            this.Name = name;  
        }
    }
}