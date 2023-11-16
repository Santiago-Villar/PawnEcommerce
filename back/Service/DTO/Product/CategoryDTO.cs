using Service.Product;
using System.Diagnostics.CodeAnalysis;

namespace Service.DTO.Product;

[ExcludeFromCodeCoverage]
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