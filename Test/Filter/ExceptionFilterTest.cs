using System.Security.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using PawnEcommerce.Middlewares;
using Service.Exception;
using RouteData = Microsoft.AspNetCore.Routing.RouteData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace Test.Filter;

[TestClass]
[ExcludeFromCodeCoverage]
public class ExceptionFilterTest
{
    public ObjectResult? TestHelper(Exception e)
    {
        var actionContext = new ActionContext(
            new DefaultHttpContext(),
            new RouteData(),
            new ActionDescriptor());

        var context = new ExceptionContext(
            actionContext,
            new List<IFilterMetadata>())
        {
            Exception = e
        };

        var filter = new ExceptionMiddleware();

        filter.OnException(context);

        var result = context.Result as ObjectResult;
        return result;
    }
    
    
    [TestMethod]
    public void OnException_RepositoryException_Returns404()
    {
        var result = TestHelper(new RepositoryException("Repository exception message"));
        
        Assert.IsNotNull(result);
        Assert.AreEqual(404, result.StatusCode);
    }
    
    [TestMethod]
    public void OnException_ServiceException_Returns400()
    {
        var result = TestHelper(new ServiceException("Service exception message"));
        
        Assert.IsNotNull(result);
        Assert.AreEqual(400, result.StatusCode);
    }
    
    [TestMethod]
    public void OnException_ModelException_Returns400()
    {
        var result = TestHelper(new ServiceException("Model exception message"));
        
        Assert.IsNotNull(result);
        Assert.AreEqual(400, result.StatusCode);
    }

    [TestMethod]
    public void OnException_InvalidCredentialsException_Returns401()
    {
        var result = TestHelper(new InvalidCredentialException("InvalidCredentials exception message"));
        
        Assert.IsNotNull(result);
        Assert.AreEqual(401, result.StatusCode);
    }
    
    [TestMethod]
    public void OnException_UnexpectedException_Returns500()
    {
        var result = TestHelper(new Exception("Unexpected exception message"));
        
        Assert.IsNotNull(result);
        Assert.AreEqual(500, result.StatusCode);
    }
}