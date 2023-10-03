using System;
using System.Security.Authentication;
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

        private const string Email = "TestEmail@gmail.com";
        private const string Password = "currentPassword";
        private const string DifferentPassword = "differentPassword";
        private const string ToUpdateAddress = "1234 Laughter Lane";
        private const string NewAddress = "101 Prankster Place";

        private User GetMockUser()
        {
            return new User()
            {
                Email = Email,
                Address = ToUpdateAddress,
                PasswordHash = Password
            };
        }

        private string HashPassword(string Password)
        {
            const int saltFactor = 12;

            var salt = BCrypt.Net.BCrypt.GenerateSalt(saltFactor);
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(Password, salt);

            return hashedPassword;
        }

        [TestMethod]
        public void CanCreateSessionService_Ok()
        {
            var mockRepository = new Mock<IUserRepository>();
            ISessionService sessionService = new SessionService(mockRepository.Object);
            Assert.IsNotNull(sessionService);
        }

        [TestMethod]
        public void CanAuthenticateUser()
        {
            var mockUser = GetMockUser();
            var mockRepository = new Mock<IUserRepository>();
            mockRepository.Setup(repo => repo.Get(Email)).Returns(mockUser);

            ISessionService sessionService = new SessionService(mockRepository.Object);
            string token = sessionService.Authenticate(Email, Password);

            Assert.IsNotNull(token);
        }

        [ExpectedException(typeof(InvalidCredentialException))]
        [TestMethod]
        public void AuthenticateWithWrongPassword_Throws()
        {
            var mockUser = GetMockUser();

            var mockRepository = new Mock<IUserRepository>();
            mockRepository.Setup(repo => repo.Get(Email)).Returns(mockUser);

            var sessionService = new SessionService(mockRepository.Object);
            string token = sessionService.Authenticate(Email, DifferentPassword);
        }

        [TestMethod]
        public void CanGetCurrentUser_Ok()
        {
            var mockUser = GetMockUser();
            var mockRepository = new Mock<IUserRepository>();
            mockRepository.Setup(repo => repo.Get(Email)).Returns(mockUser);

            ISessionService sessionService = new SessionService(mockRepository.Object);
            string token = sessionService.Authenticate(Email, Password);
            Console.WriteLine(token);
            User userGot = sessionService.GetCurrentUser(token);


            Assert.IsNotNull(userGot);
        }

        [TestMethod]
        public void GetCurrentUserWithWrongReturns_Null()
        {
            var mockUser = GetMockUser();
            var mockRepository = new Mock<IUserRepository>();
            mockRepository.Setup(repo => repo.Get(Email)).Returns(mockUser);

            ISessionService sessionService = new SessionService(mockRepository.Object);
            string token = sessionService.Authenticate(Email, Password);
            string wrongToken = token + "asdasd";

            User userGot = sessionService.GetCurrentUser(wrongToken);
            Assert.IsNull(userGot);
        }


    }
}

