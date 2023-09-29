using System;
using Service.User;
namespace Service.Session
{
	public interface ISessionService
	{
		public IUserRepository _repository { get; set; }

		public string Authenticate(string email, string password);

		public IUser? GetCurrentUser(string token);

    }
}

