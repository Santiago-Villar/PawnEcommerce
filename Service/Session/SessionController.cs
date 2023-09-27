using System;
namespace Service.Session
{
	public class SessionController
	{
		private ISessionService _sessionService { get; set; }

        public SessionController(ISessionService sessionService)
		{
			_sessionService = sessionService;
		}
	}
}

