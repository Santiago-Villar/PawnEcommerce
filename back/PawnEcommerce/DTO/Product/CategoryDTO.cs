using Service.Product;
using System.Diagnostics.CodeAnalysis;

namespace PawnEcommerce.DTO.Product;

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