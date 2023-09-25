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
        mockRepository.Setup(repo => repo.Get(mockUser.Object.Email)).Returns(mockUser.Object);
        
        var userService = new UserService(mockRepository.Object);
        userService.SignUp(mockUser.Object);
    }
    
    [ExpectedException(typeof(RepositoryException))]
    [TestMethod]
    public void CanLogInUser_UnregisteredUser_Throw()
    {
        const string email = "TestEmail@gmail.com";
        const string password = "currentPassword";

        var mockUser = new Mock<IUser>();
        mockUser.Setup(user => user.Email).Returns(email);

        var mockRepository = new Mock<IUserRepository>();
        mockRepository.Setup(repo => repo.Get(mockUser.Object.Email)).Returns(() => null);
        
        var userService = new UserService(mockRepository.Object);
        userService.LogIn(email, password);
    }
    
    [ExpectedException(typeof(RepositoryException))]
    [TestMethod]
    public void CanLogInUser_WrongPassword_Throw()
    {
        const string email = "TestEmail@gmail.com";
        const string password = "differentPassword";
        
        var mockUser = new Mock<IUser>();
        mockUser.Setup(user => user.Email).Returns(email);
        mockUser.Setup(user => user.PasswordHash).Returns(HashPassword("currentPassword"));

        var mockToCheckUser = new Mock<IUser>();
        mockToCheckUser.Setup(user => user.Email).Returns(email);
        mockToCheckUser.Setup(user => user.PasswordHash).Returns(HashPassword(password));
        
        var mockRepository = new Mock<IUserRepository>();
        mockRepository.Setup(repo => repo.Get(email)).Returns(mockUser.Object);

        var userService = new UserService(mockRepository.Object);
        userService.LogIn(email, password);
    }
    
    [TestMethod]
    public void CanLogInUser_Ok()
    {
        const string email = "TestEmail@gmail.com";
        const string password = "currentPassword";

        var mockUser = new Mock<IUser>();
        mockUser.Setup(user => user.Email).Returns(email);
        mockUser.Setup(user => user.PasswordHash).Returns(HashPassword(password));

        var mockRepository = new Mock<IUserRepository>();
        mockRepository.Setup(repo => repo.Get(email)).Returns(mockUser.Object);

        var userService = new UserService(mockRepository.Object);
        var loggedInUser = userService.LogIn(email, password);
        
        Assert.AreEqual(mockUser.Object, loggedInUser);
    }

    private string HashPassword(string password)
    {
        const int saltFactor = 12;
            
        var salt = BCrypt.Net.BCrypt.GenerateSalt(saltFactor);
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);

        return hashedPassword;
    }
        
    [ExpectedException(typeof(RepositoryException))]
    [TestMethod]
    public void CanDeleteUser_AlreadyDeletedUser_Throw()
    {
        const string email = "TestEmail@gmail.com";
        const string password = "currentPassword";

        var mockUser = new Mock<IUser>();
        mockUser.Setup(user => user.Email).Returns(email);
        mockUser.Setup(user => user.PasswordHash).Returns(HashPassword(password));

        var mockRepository = new Mock<IUserRepository>();
        mockRepository.Setup(repo => repo.Get(email)).Returns(() => null);

        var userService = new UserService(mockRepository.Object);
        userService.DeleteUser(mockUser.Object);
    }
    
    [TestMethod]
    public void CanDeleteUser_Ok()
    {
        const string email = "TestEmail@gmail.com";
        const string password = "currentPassword";

        var mockUser = new Mock<IUser>();
        mockUser.Setup(user => user.Email).Returns(email);
        mockUser.Setup(user => user.PasswordHash).Returns(HashPassword(password));

        var mockRepository = new Mock<IUserRepository>();
        mockRepository.Setup(repo => repo.Get(email)).Returns(mockUser.Object);

        var userService = new UserService(mockRepository.Object);
        userService.DeleteUser(mockUser.Object);
    }
}