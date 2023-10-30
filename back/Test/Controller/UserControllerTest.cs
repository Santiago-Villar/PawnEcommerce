using Microsoft.AspNetCore.Mvc;
using Moq;
using PawnEcommerce.Controllers;
using Service.User;
using Service.DTO.User;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using Service.DTO.Product;
using Service.Product;

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
        Assert.IsInstanceOfType(result.Value, typeof(User));

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