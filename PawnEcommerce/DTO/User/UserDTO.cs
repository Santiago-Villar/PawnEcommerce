using Service.User.Role;

namespace PawnEcommerce.DTO.User
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
        public string Email { get; set; }
    }

}
