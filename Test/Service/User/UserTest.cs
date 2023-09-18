using Service.Exception;
using Service.User;
using Moq;

namespace Test;

[TestClass]
public class UserTest
{
    [TestMethod]
    public void CanCreateUser_Ok()
    {
        var user = new User();
        Assert.IsNotNull(user);
    }
        
    [TestMethod]
    public void CanSetEmail_Ok()
    {
        var user = new User()
        {
            Email = "test@email.com"
        };
        
        Assert.AreEqual("test@email.com", user.Email);
    }

    [ExpectedException(typeof(ModelException))]
    [TestMethod]
    public void CanSetEmail_WrongSyntax_Throws()
    {
        var user = new User()
        {
            Email = "testEmail"
        };
    }

    [ExpectedException(typeof(RepositoryException))]
    [TestMethod]
    public void CanCreateUser_RepeatedEmail_Throws()
    {
        var user = new User()
        {
            Email = "alreadyExistingEmail@ort.com"
        };
        
        var mockUserRepository = new Mock<IUserRepository>();
        mockUserRepository.Setup(userRepo => userRepo.Add(user))
            .Throws(new RepositoryException("Email already exists"));

        var mockedRepo = mockUserRepository.Object;
        mockedRepo.Add(user);
    }
    
    

    
}
