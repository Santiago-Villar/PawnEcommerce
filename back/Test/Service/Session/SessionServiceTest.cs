using System;
using System.Security.Authentication;
using Moq;
using Service.User;
using Service.Session;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Test.Service.Session
{
    [TestClass]
    [ExcludeFromCodeCoverage]
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
            var mockHttpContextAccessor = GetMockHttpContextAccessor("");
            ISessionService sessionService = new SessionService(mockRepository.Object, mockHttpContextAccessor.Object);
            Assert.IsNotNull(sessionService);
        }

        [TestMethod]
        public void CanAuthenticateUser()
        {
            var mockUser = GetMockUser();
            var mockRepository = new Mock<IUserRepository>();
            mockRepository.Setup(repo => repo.Get(Email)).Returns(mockUser);

            var mockHttpContextAccessor = GetMockHttpContextAccessor(""); 
            ISessionService sessionService = new SessionService(mockRepository.Object, mockHttpContextAccessor.Object);
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

            var mockHttpContextAccessor = GetMockHttpContextAccessor(""); 
            var sessionService = new SessionService(mockRepository.Object, mockHttpContextAccessor.Object);
            string token = sessionService.Authenticate(Email, DifferentPassword);
        }

        [TestMethod]
        public void CanGetCurrentUser_Ok()
        {
            var mockUser = GetMockUser();
            var mockRepository = new Mock<IUserRepository>();
            mockRepository.Setup(repo => repo.Get(Email)).Returns(mockUser);

            var token = new SessionService(mockRepository.Object, GetMockHttpContextAccessor("").Object).Authenticate(Email, Password);  

            var mockHttpContextAccessor = GetMockHttpContextAccessor(token); 
            ISessionService sessionService = new SessionService(mockRepository.Object, mockHttpContextAccessor.Object); 

            User userGot = sessionService.GetCurrentUser();

            Assert.IsNotNull(userGot);
        }

        [TestMethod]
        public void GetCurrentUserWithWrongReturns_Null()
        {
            var mockUser = GetMockUser();
            var mockRepository = new Mock<IUserRepository>();
            mockRepository.Setup(repo => repo.Get(Email)).Returns(mockUser);

            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            var mockHttpContext = new Mock<HttpContext>();
            var mockHttpRequest = new Mock<HttpRequest>();

            mockHttpRequest.SetupGet(req => req.Headers).Returns(new HeaderDictionary {
                { "Authorization", new StringValues("Bearer " + "YourFakeTokenHere") }
            });

            mockHttpContext.SetupGet(ctx => ctx.Request).Returns(mockHttpRequest.Object);
            mockHttpContextAccessor.SetupGet(accessor => accessor.HttpContext).Returns(mockHttpContext.Object);

            ISessionService sessionService = new SessionService(mockRepository.Object, mockHttpContextAccessor.Object);
            string token = sessionService.Authenticate(Email, Password);
            string wrongToken = token + "asdasd";

            User userGot = sessionService.GetCurrentUser();
            Assert.IsNull(userGot);
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

