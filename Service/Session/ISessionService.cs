using System;
namespace Service.Session
{
	public interface ISessionService
	{
		public string Authenticate(string email, string password);
	}
}

