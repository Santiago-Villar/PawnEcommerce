using Service.User;
using Service.User.Role;

namespace PawnEcommerce.DTO;

public class UserCreateModel
{
    public string Email { get; set; }
    public string Password { get; set; }

    public IUser ToEntity()
    {
        return new User()
        {
            Email = Email,
            PasswordHash = this.Password,
            Roles = { RoleType.User }
        };
    }
}