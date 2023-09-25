using Moq;
using Service.User;
using Moq;

namespace Test.Service.UserTest;

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

    [TestMethod]
    public void CanSignUpUser_Ok()
    {
        var mockUser = new Mock<IUser>();
        mockUser.Setup(user => user.Email).Returns("TestEmail@gmail.com");
        var mockRepository = new Mock<IUserRepository>();
        
        var userService = new UserService(mockRepository.Object);
        userService.SignUp(mockUser.Object);
    }
}