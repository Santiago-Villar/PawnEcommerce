using Service.User.Role;
using System.Diagnostics.CodeAnalysis;

namespace PawnEcommerce.DTO.User
{
    [ExcludeFromCodeCoverage]
    public class UserDTO
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
        public string Email { get; set; }
    }

}
