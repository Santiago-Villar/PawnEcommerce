using System;
using Moq;
using Service.User;
using Service.Session;

namespace Test.Service.Session
{
	public class SessionControllerTest
	{
		public SessionControllerTest()
		{
		}

        [TestMethod]
        public void CanCreateController_Ok()
        {
            Mock<ISessionService> userServiceMock = new Mock<ISessionService>();
            var sessionController = new SessionController(userServiceMock.Object);
            Assert.IsNotNull(sessionController);
        }
    }
}

