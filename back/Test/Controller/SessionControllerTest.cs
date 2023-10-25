using System;
using Moq;
using Service.User;
using Service.Session;
using Microsoft.AspNetCore.Mvc;
using PawnEcommerce.Controllers;
using PawnEcommerce.DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Test.Service.Session
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class SessionControllerTest
	{

        private const string Email = "TestEmail@gmail.com";
        private const string Email2 = "Test2Email@gmail.com";
        private const string Password = "currentPassword";
        private const string DifferentPassword = "differentPassword";
        private const string ToUpdateAddress = "1234 Laughter Lane";
        private const string NewAddress = "101 Prankster Place";

        private User GetMockUser()
        {
            return new User
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

        private readonly Mock<IUserRepository> _mockRepository;
        private readonly SessionService _sessionService;
        private readonly SessionController _sessionController;
        private readonly User _mockUser;

        public SessionControllerTest()
        {
            _mockUser = GetMockUser();
            _mockRepository = new Mock<IUserRepository>();
            _mockRepository.Setup(repo => repo.Get(It.IsAny<string>())).Returns((string email) => email == Email ? _mockUser : null);
            _sessionService = new SessionService(_mockRepository.Object,GetMockHttpContextAccessor("").Object);
            _sessionController = new SessionController(_sessionService);
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
            var request = new LoginRequest()
            {
                Email = Email,
                Password = Password
            };

            var result = _sessionController.Login(request);

            var objectResult = result as ObjectResult;

            Assert.IsNotNull(objectResult);
            Assert.AreEqual(200, objectResult.StatusCode);

            var loginResponse = objectResult.Value as LoginResponse;

            Assert.IsNotNull(loginResponse);

            var token = loginResponse.Token;
            Assert.IsFalse(string.IsNullOrEmpty(token));

        }
        [TestMethod]
        public void LoginWithWrongMail_ReturnsUnauthorized()
        {
            var request = new LoginRequest()
            {
                Email = Email2,
                Password = Password
            };

            var result = _sessionController.Login(request);
            var objectResult = result as ObjectResult;

            Assert.IsNotNull(objectResult);
            Assert.AreEqual(401, objectResult.StatusCode);
        }
        private Mock<IHttpContextAccessor> GetMockHttpContextAccessor(string authorizationHeader)
        {
            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            var mockHttpContext = new Mock<HttpContext>();
            var mockHttpRequest = new Mock<HttpRequest>();

            mockHttpRequest.SetupGet(req => req.Headers).Returns(new HeaderDictionary {
                { "Authorization", new StringValues("Bearer " + authorizationHeader) }
            });

            mockHttpContext.SetupGet(ctx => ctx.Request).Returns(mockHttpRequest.Object);
            mockHttpContextAccessor.SetupGet(accessor => accessor.HttpContext).Returns(mockHttpContext.Object);

            return mockHttpContextAccessor;
        }
    }
}

