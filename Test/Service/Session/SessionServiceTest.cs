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
            ISessionService userService = new SessionService();
            Assert.IsNotNull(userService);
        }

        [TestMethod]
        public void CanAuthenticateUser()
        {
            var mockUser = GetMockUser();
            var mockRepository = new Mock<IUserRepository>();
            mockRepository.Setup(repo => repo.Get(Email)).Returns(mockUser);

            ISessionService userService = new SessionService();
            string token = userService.Authenticate(Email, Password);

            Assert.IsNotNull(token);
        }
    }
}

