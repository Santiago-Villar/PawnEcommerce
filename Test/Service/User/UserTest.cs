namespace Test;
using Service.User;

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
            Email = "testEmail"
        };
        
        Assert.AreEqual("testEmail", user.Email);
    }

    
}
