using Microsoft.AspNetCore.Mvc;
using Moq;
using PawnEcommerce.Controllers;
using Service.User;
using Service.DTO.User;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using Service.Session;
using Microsoft.Extensions.DependencyInjection;

namespace Test.Controller;

[TestClass]
[ExcludeFromCodeCoverage]
public class UserControllerTest
{
    [TestMethod]
    [ExcludeFromCodeCoverage]
    public void CanCreateController_Ok()
    {
        Mock<IUserService> userServiceMock = new Mock<IUserService>();
        Mock<IServiceProvider> serviceProviderMock = GetServiceProvider();

        var userController = new UserController(userServiceMock.Object, serviceProviderMock.Object);
        Assert.IsNotNull(userController);
    }
    
    [TestMethod]
    public void SignUp_Ok()
    {
        var userServiceMock = new Mock<IUserService>();
        Mock<IServiceProvider> serviceProviderMock = GetServiceProvider();

        var userController = new UserController(userServiceMock.Object, serviceProviderMock.Object);
        var newUser = new UserCreateModel()
        {
            Email = "testEmail@gmail.com",
            Password = "secret",
        };

        var result = userController.SignUp(newUser) as OkResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);
    }

    [TestMethod]
    public void Update_Ok()
    {
        var userServiceMock = new Mock<IUserService>();
        Mock<IServiceProvider> serviceProviderMock = GetServiceProvider();

        var userController = new UserController(userServiceMock.Object, serviceProviderMock.Object);


        var expectedUser = new User
        {
            Id = 1,
            Email = "testEmail@gmail.com"
        };
        userServiceMock.Setup(ps => ps.UpdateUserUsingDTO(It.IsAny<int>(), It.IsAny<UserUpdateModel>()))
                       .Returns(expectedUser);

        var updateUser = new UserUpdateModel()
        {
            Email = "testEmail@gmail.com",
        };

        var result = userController.Update(1, updateUser) as OkObjectResult;
        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);
        Assert.IsInstanceOfType(result.Value, typeof(UserDTO));

    }

    [TestMethod]
    public void Delete_ReturnsOkResult()
    {
        var userServiceMock = new Mock<IUserService>();
        Mock<IServiceProvider> serviceProviderMock = GetServiceProvider();

        var userController = new UserController(userServiceMock.Object, serviceProviderMock.Object);
        var deleteUser = new UserCreateModel()
        {
            Email = "testEmail@gmail.com",
            Password = "secret",
        };

        var result = userController.Delete(1) as OkResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);
    }

    [TestMethod]
    public void GetProfile_OkResult()
    {
        var userServiceMock = new Mock<IUserService>();
        Mock<IServiceProvider> serviceProviderMock = GetServiceProvider(new User { Id = 1 });

        var controller = new UserController(userServiceMock.Object, serviceProviderMock.Object);

        var result = controller.GetProfile() as OkObjectResult;
        var user = result.Value as UserDTO;

        Assert.AreEqual(200, result.StatusCode);
        Assert.AreEqual(1, user.Id);
    }

    [TestMethod]
    public void GetProfile_BadRequestResult()
    {
        var userServiceMock = new Mock<IUserService>();
        Mock<IServiceProvider> serviceProviderMock = GetServiceProvider();

        var controller = new UserController(userServiceMock.Object, serviceProviderMock.Object);

        var result = controller.GetProfile() as BadRequestObjectResult;

        Assert.AreEqual(400, result.StatusCode);
    }

    private static Mock<IServiceProvider> GetServiceProvider(User? currentUser = null)
    {
        var serviceProviderMock = new Mock<IServiceProvider>();

        var serviceScopeMock = new Mock<IServiceScope>();

        var serviceScopeFactoryMock = new Mock<IServiceScopeFactory>();

        serviceProviderMock.Setup(sp => sp.GetService(typeof(IServiceScopeFactory)))
            .Returns(serviceScopeFactoryMock.Object);

        serviceScopeFactoryMock.Setup(ssf => ssf.CreateScope())
            .Returns(serviceScopeMock.Object);

        var sessionServiceMock = new Mock<ISessionService>();
        sessionServiceMock.Setup(ss => ss.GetCurrentUser()).Returns(currentUser);

        sessionServiceMock.Setup(ss => ss.ExtractUserIdFromToken(It.IsAny<string>()))
            .Returns((string token) => 1);

        serviceScopeMock.Setup(scope => scope.ServiceProvider.GetService(typeof(ISessionService)))
            .Returns(sessionServiceMock.Object);

        return serviceProviderMock;
    }
}