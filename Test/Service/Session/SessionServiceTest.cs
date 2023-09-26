using System;
using Moq;
using Service.User;

namespace Test.Service.Session
{
    [TestClass]
    public class SessionServiceTest
	{
		public SessionServiceTest()
		{
		}

        [TestMethod]
        public void CanCreateUserService_Ok()
        {
            ISessionService userService = new SessionService();
            Assert.IsNotNull(userService);
        }
    }
}

