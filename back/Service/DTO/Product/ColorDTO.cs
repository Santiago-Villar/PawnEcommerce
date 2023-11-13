using Service.Product;
using System.Diagnostics.CodeAnalysis;

namespace Service.DTO.Product;

[ExcludeFromCodeCoverage]
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