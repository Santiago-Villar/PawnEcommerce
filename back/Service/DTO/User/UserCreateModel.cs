using Service;
using Service.User.Role;
using System.Diagnostics.CodeAnalysis;

namespace Service.DTO.User;

[ExcludeFromCodeCoverage]
public class UserCreateModel
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string Address { get; set; }

    public Service.User.User ToEntity()
    {
        return new Service.User.User()
        {
            Email = Email,
            PasswordHash = Password,
            Address = Address,
            Roles = { RoleType.User }
        };
    }
}