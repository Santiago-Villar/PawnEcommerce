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
    
}
