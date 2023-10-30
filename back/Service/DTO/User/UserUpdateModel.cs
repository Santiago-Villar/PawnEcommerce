using Service.User.Role;
using System.Diagnostics.CodeAnalysis;

namespace Service.DTO.User
{
    [ExcludeFromCodeCoverage]
    public class UserUpdateModel
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Address { get; set; }

    }
}
