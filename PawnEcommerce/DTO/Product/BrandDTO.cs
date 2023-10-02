using Service.Product;

namespace PawnEcommerce.DTO.Product;

public class BrandDTO
{
    public int Id { get; set; }
    public String Name { get; set; }
    
    public Brand ToEntity()
    {
        return new Brand(Id)
        {
            Name = Name,
        };
    }
}