using Microsoft.AspNetCore.Mvc;
using Moq;
using PawnEcommerce.Controllers;
using Service.User;
using Service.DTO.User;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

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
        var userController = new UserController(userServiceMock.Object);
        Assert.IsNotNull(userController);
    }
    
    [TestMethod]
    public void SignUp_Ok()
    {
        var userServiceMock = new Mock<IUserService>();
        var userController = new UserController(userServiceMock.Object);
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
        var userController = new UserController(userServiceMock.Object);
        var updateUser = new UserCreateModel()
        {
            Email = "testEmail@gmail.com",
            Password = "secret",
        };

        var result = userController.Update(1, updateUser) as OkResult;
        
        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);
    }

    [TestMethod]
    public void Delete_ReturnsOkResult()
    {
        var userServiceMock = new Mock<IUserService>();
        var userController = new UserController(userServiceMock.Object);
        var deleteUser = new UserCreateModel()
        {
            Email = "testEmail@gmail.com",
            Password = "secret",
        };

        var result = userController.Delete(1) as OkResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);
    }
}