using System;
using Moq;
using Service.User;
using Service.Session;

namespace Test.Service.Session
{
    [TestClass]
    public class SessionServiceTest
	{
		public SessionServiceTest()
		{
		}

        [TestMethod]
        public void CanCreateSessionService_Ok()
        {
            ISessionService userService = new SessionService();
            Assert.IsNotNull(userService);
        }
    }
}

