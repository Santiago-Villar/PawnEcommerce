namespace Service.Product
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Product(String name, String Description) {
            this.Name = name;  
            this.Description = Description;
        }
    }
}