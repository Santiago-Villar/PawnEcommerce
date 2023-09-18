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
        const string email = "test@email.com";
        
        var user = new User()
        {
            Email = email
        };
        
        Assert.AreEqual(email, user.Email);
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
    
    [TestMethod]
    public void CanSetAddress_Ok()
    {
        const string address = "Loch Ness Road, Towson, MD.";
        
        var user = new User()
        {
            Address = address
        };
        
        Assert.AreEqual(address, user.Address);
    }
    
    [TestMethod]
    public void CanSetPasswordHash_Ok()
    {
        const string password = "SafeSecret";
        
        var user = new User()
        {
            PasswordHash = password
        };
        
        var isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);

        Assert.IsTrue(isPasswordValid);
    }
    
}
