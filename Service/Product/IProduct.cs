using Service.Product;

namespace Service.Product;

public interface IProduct
{
    int Id { get; set; }
   
    string Name { get; set; }
    
    string Description { get; set; }
    
    int Price { get; set; }

    string BrandName { get; set; }
    Brand Brand { get; set; }

    string CategoryName { get; set; }
    Category Category { get; set; }

    ICollection<Color> Colors { get; set; }

    void AddColor(Color color);
}

