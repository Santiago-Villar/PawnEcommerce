using Service.Product;
using System.Diagnostics.CodeAnalysis;

namespace Service.DTO.Product;

[ExcludeFromCodeCoverage]
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