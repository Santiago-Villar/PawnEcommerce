using System;
using Service.User.Role;

namespace Service.User
{
	public interface IUser
	{
		public string Address { get; set; }
        string Email { get; set; }
        string PasswordHash { get; init; }
        public List<RoleType> Roles { get; set; }
        public ICollection<Service.Sale.Sale> Sales { get; set; }
    }
}

