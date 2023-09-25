using Moq;
using Service.User;
using Moq;
using Service.Exception;

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
    
    [ExpectedException(typeof(RepositoryException))]
    [TestMethod]
    public void CanSignUpUser_RepeatedUser_Throw()
    {
        var mockUser = new Mock<IUser>();
        mockUser.Setup(user => user.Email).Returns("TestEmail@gmail.com");
        var mockRepository = new Mock<IUserRepository>();
        mockRepository.Setup(repo => repo.Exists(mockUser.Object)).Returns(true);
        
        var userService = new UserService(mockRepository.Object);
        userService.SignUp(mockUser.Object);
    }
    
    [ExpectedException(typeof(RepositoryException))]
    [TestMethod]
    public void CanLogInUser_UnregisteredUser_Throw()
    {
        var mockUser = new Mock<IUser>();
        mockUser.Setup(user => user.Email).Returns("TestEmail@gmail.com");
        var mockRepository = new Mock<IUserRepository>();
        mockRepository.Setup(repo => repo.Exists(mockUser.Object)).Returns(false);
        
        var userService = new UserService(mockRepository.Object);
        userService.LogIn(mockUser.Object);
    }
}