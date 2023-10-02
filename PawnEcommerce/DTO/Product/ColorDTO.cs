using Service.Product;

namespace PawnEcommerce.DTO.Product;

public class ColorDTO
{
    public int Id { get; set; }
    public String Name { get; set; }
    public String Code { get; set; }
    
    public Color ToEntity()
    {
        return new Color(Id)
        {
            Name = Name,
            Code = Code
        };
    }
}