using Service.User.UserService;

namespace Test;

[TestClass]
public class UserServiceTest
{
    [TestMethod]
    public void CanCreateUserService_Ok()
    {
        var user = new UserService();
        Assert.IsNotNull(user);
    }
}