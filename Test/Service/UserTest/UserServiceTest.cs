using System.Security.Authentication;
using Moq;
using Service.User;
using Service.Exception;

namespace Test.Service.UserTest;

[TestClass]
public class UserServiceTest
{
    private const string Email = "TestEmail@gmail.com";
    private const string Password = "currentPassword";
    private const string DifferentPassword = "differentPassword";
    private const string ToUpdateAddress = "1234 Laughter Lane";
    private const string NewAddress = "101 Prankster Place";

    private User GetMockUser()
    {
        return new User()
        {
            Id = 1,
            Email = Email,
            Address = ToUpdateAddress,
            PasswordHash = Password
        };
    }
    
    private User GetSecondaryMockUser()
    {
        return new User()
        {
            Id = 2,
            Email = Email,
            Address = NewAddress,
            PasswordHash = DifferentPassword
        };
    }
    
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
        var mockUser = GetMockUser();
        var mockRepository = new Mock<IUserRepository>();
        
        var userService = new UserService(mockRepository.Object);
        userService.SignUp(mockUser);
    }
    
    [ExpectedException(typeof(ServiceException))]
    [TestMethod]
    public void CanSignUpUser_RepeatedUser_Throw()
    {
        var mockUser = GetMockUser();
        
        var mockRepository = new Mock<IUserRepository>();
        mockRepository.Setup(repo => repo.Get(mockUser.Email)).Returns(mockUser);
        
        var userService = new UserService(mockRepository.Object);
        userService.SignUp(mockUser);
    }
    
    [ExpectedException(typeof(RepositoryException))]
    [TestMethod]
    public void CanLogInUser_UnregisteredUser_Throw()
    {
        var mockUser = GetMockUser();

        var mockRepository = new Mock<IUserRepository>();
        mockRepository.Setup(repo => repo.Get(mockUser.Email)).Returns(() => null);
        
        var userService = new UserService(mockRepository.Object);
        userService.LogIn(Email, Password);
    }
    
    [ExpectedException(typeof(InvalidCredentialException))]
    [TestMethod]
    public void CanLogInUser_WrongPassword_Throw()
    {
        var mockUser = GetMockUser();
        
        var mockRepository = new Mock<IUserRepository>();
        mockRepository.Setup(repo => repo.Get(Email)).Returns(mockUser);

        var userService = new UserService(mockRepository.Object);
        userService.LogIn(Email, DifferentPassword);
    }
    
    [TestMethod]
    public void CanLogInUser_Ok()
    {
        var mockUser = GetMockUser();

        var mockRepository = new Mock<IUserRepository>();
        mockRepository.Setup(repo => repo.Get(Email)).Returns(mockUser);

        var userService = new UserService(mockRepository.Object);
        var loggedInUser = userService.LogIn(Email, Password);
        
        Assert.AreEqual(mockUser, loggedInUser);
    }

    private string HashPassword(string Password)
    {
        const int saltFactor = 12;
            
        var salt = BCrypt.Net.BCrypt.GenerateSalt(saltFactor);
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(Password, salt);

        return hashedPassword;
    }
        
    [ExpectedException(typeof(RepositoryException))]
    [TestMethod]
    public void CanDeleteUser_AlreadyDeletedUser_Throw()
    {
        var mockUser = GetMockUser();

        var mockRepository = new Mock<IUserRepository>();
        mockRepository.Setup(repo => repo.Get(1)).Returns(() => null);

        var userService = new UserService(mockRepository.Object);
        userService.DeleteUser(mockUser.Id);
    }
    
    [TestMethod]
    public void CanDeleteUser_Ok()
    {
        var mockUser = GetMockUser();

        var mockRepository = new Mock<IUserRepository>();
        mockRepository.Setup(repo => repo.Get(1)).Returns(mockUser);

        var userService = new UserService(mockRepository.Object);
        userService.DeleteUser(mockUser.Id);
    }
    
    [ExpectedException(typeof(RepositoryException))]
    [TestMethod]
    public void CanUpdateUser_NotExistingUser_Throw()
    {
        var toUpdateMockUser = GetMockUser();
        var mockUser = GetSecondaryMockUser();
        
        var mockRepository = new Mock<IUserRepository>();
        mockRepository.Setup(repo => repo.Get(1)).Returns(() => null);
        mockRepository.Setup(repo => repo.Update(toUpdateMockUser));
        
        var userService = new UserService(mockRepository.Object);
        userService.UpdateUser(mockUser);
    }
    
    [TestMethod]
    public void CanGetUser_Ok()
    {
        var user = GetMockUser();
        
        var mockRepository = new Mock<IUserRepository>();
        mockRepository.Setup(repo => repo.Get(1)).Returns(user);
        
        var userService = new UserService(mockRepository.Object);
        var foundUser = userService.Get(1);
        
        Assert.AreEqual(user, foundUser);
    }
    
    [ExpectedException(typeof(RepositoryException))]
    [TestMethod]
    public void CanGetUser_Throw()
    {
        var user = GetMockUser();
        
        var mockRepository = new Mock<IUserRepository>();
        mockRepository.Setup(repo => repo.Get(9)).Returns(user);
        
        var userService = new UserService(mockRepository.Object);
        var foundUser = userService.Get(1);
    }
}