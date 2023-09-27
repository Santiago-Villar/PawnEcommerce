using System;
namespace Service.Session
{
	public class SessionService : ISessionService
	{
		public SessionService()
		{
		}

        public string Authenticate(string email, string password)
        {
            return "a";
        }
    }
}

