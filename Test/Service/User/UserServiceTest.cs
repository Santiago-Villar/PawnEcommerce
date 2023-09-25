using Moq;
using Service.User;

namespace Test.Service.User;

[TestClass]
public class UserServiceTest
{
    [TestMethod]
    public void CanCreateUserService_Ok()
    {
        var mockRepository = new Mock<IUserRepository>();
        var userService = new UserService(mockRepository.Object);
        Assert.IsNotNull(userService);
    }

}