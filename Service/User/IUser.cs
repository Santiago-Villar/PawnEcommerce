using System;
namespace Service.User
{
	public interface IUser
	{
        string Email { get; set; }
        string PasswordHash { get; init; }
    }
}

