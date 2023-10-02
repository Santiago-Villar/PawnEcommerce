using Service.Product;

namespace PawnEcommerce.DTO.Product;

public class CategoryDTO
{
    public int Id { get; set; }
    public String Name { get; set; }
    
    public Category ToEntity()
    {
        return new Category(Id)
        {
            Name = Name,
        };
    }
}