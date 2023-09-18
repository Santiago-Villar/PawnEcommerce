using Service.Exception;
using Service.User;

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
    
    

    
}
