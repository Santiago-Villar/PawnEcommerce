using Service.User.Role;

namespace PawnEcommerce.DTO.User
{
    public class UserUpdateModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Adress { get; set; }

        public Service.User.User ToEntity()
        {
            return new Service.User.User()
            {
                Email = Email,
                PasswordHash = Password,
                Address = Adress,
                Roles = { RoleType.User }
            };
        }
    }
}
