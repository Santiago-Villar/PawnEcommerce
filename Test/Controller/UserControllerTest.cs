using Microsoft.AspNetCore.Mvc;
using Moq;
using PawnEcommerce.Controllers;
using PawnEcommerce.DTO;
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
    
    [TestMethod]
    public void SignUp_Ok()
    {
        var userServiceMock = new Mock<IUserService>();
        var userController = new UserController(userServiceMock.Object);
        var newUser = new UserCreateModel()
        {
            Email = "testEmail@gmail.com",
            Password = "secret",
            Address = "Lauro Müller 1776"
        };

        var result = userController.SignUp(newUser) as OkResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);
    }
    
    public void Update_Ok()
    {
        var userServiceMock = new Mock<IUserService>();
        var userController = new UserController(userServiceMock.Object);
        var updateUser = new UserCreateModel()
        {
            Email = "testEmail@gmail.com",
            Password = "secret",
            Address = "4116 Pretty View Lane"
        };

        var result = userController.Update(updateUser) as OkResult;
        
        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);
    }
    
    public void Delete_ReturnsOkResult()
    {
        var userServiceMock = new Mock<IUserService>();
        var userController = new UserController(userServiceMock.Object);
        var deleteUser = new UserCreateModel()
        {
            Email = "testEmail@gmail.com",
            Password = "secret",
            Address = "4116 Pretty View Lane"
        };

        var result = userController.Delete(deleteUser) as OkResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);
    }
}