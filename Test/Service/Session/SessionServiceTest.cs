using System;
using Moq;
using Service.User;
using Service.Session;
using Service.Exception;

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

        private IUser GetMockUser()
        {
            var mockUser = new Mock<IUser>();
            mockUser.Setup(user => user.Email).Returns(Email);
            mockUser.Setup(user => user.Address).Returns(ToUpdateAddress);
            mockUser.Setup(user => user.PasswordHash).Returns(HashPassword(Password));

            return mockUser.Object;
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

        [ExpectedException(typeof(RepositoryException))]
        [TestMethod]
        public void AuthenticateWithWrongPassword_Throws()
        {
            var mockUser = GetMockUser();

            var mockRepository = new Mock<IUserRepository>();
            mockRepository.Setup(repo => repo.Get(Email)).Returns(mockUser);

            var sessionService = new SessionService(mockRepository.Object);
            string token = sessionService.Authenticate(Email, DifferentPassword);
        }
    }
}

