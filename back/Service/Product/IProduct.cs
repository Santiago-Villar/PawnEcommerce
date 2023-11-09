using Service.Product;

namespace Service.Product;

public interface IProduct
{
    int Id { get; set; }
   
    string Name { get; set; }
    
    string Description { get; set; }
    
    int Price { get; set; }

    int BrandId { get; set; }
    Brand Brand { get; set; }

    int Stock {  get; set; }

    string PaymentMethod {  get; set; }

    int CategoryId { get; set; }
    Category Category { get; set; }

    IEnumerable<Color> Colors { get; }
    ICollection<ProductColor> ProductColors { get; set; }

    void AddColor(Color color);
}

