using System;
namespace Service.User
{
	public interface IUser
	{
		public string Address { get; set; }
        string Email { get; set; }
        string PasswordHash { get; init; }

        public ICollection<Service.Sale.Sale> Sales { get; set; }
    }
}

