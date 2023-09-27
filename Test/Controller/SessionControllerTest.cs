using System;
using Moq;
using Service.User;
using Service.Session;
using Microsoft.AspNetCore.Mvc;
using PawnEcommerce.Controllers;
using PawnEcommerce.DTO;

namespace Test.Service.Session
{
    [TestClass]
    public class SessionControllerTest
	{
		public SessionControllerTest()
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
        public void CanCreateController_Ok()
        {
            Mock<ISessionService> userServiceMock = new Mock<ISessionService>();
            var sessionController = new SessionController(userServiceMock.Object);
            Assert.IsNotNull(sessionController);
        }

        [TestMethod]
        public void Login_Ok()
        {

            var mockUser = GetMockUser();
            var mockRepository = new Mock<IUserRepository>();
            mockRepository.Setup(repo => repo.Get(Email)).Returns(mockUser);
            var sessionService = new SessionService(mockRepository.Object);
            var sessionController = new SessionController(sessionService);


            var result = sessionController.Login(Email, Password);

            var token = (result.Value as dynamic)?.token as string;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsFalse(string.IsNullOrEmpty(token));

        }
    }
}

