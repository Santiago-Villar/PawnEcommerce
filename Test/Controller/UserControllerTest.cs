using Moq;
using PawnEcommerce.Controllers;
using Service.User;

namespace Test.Controller;

[TestClass]
public class UserControllerTest
{
    [TestMethod]
    public void CanCreateController_Ok()
    {
        Mock<IUserService> userServiceMock = new Mock<IUserService>();
        var userController = new UserController(userServiceMock.Object);
        Assert.IsNotNull(userController);
    }
}