using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using PawnEcommerce.Middlewares;
using Service.Exception;
using RouteData = Microsoft.AspNetCore.Routing.RouteData;

namespace Test.Filter;

[TestClass]
public class ExceptionFilterTest
{
    [TestMethod]
    public void OnException_RepositoryException_Returns404()
    {
        var actionContext = new ActionContext(
            new DefaultHttpContext(),
            new RouteData(),
            new ActionDescriptor());

        var context = new ExceptionContext(
            actionContext,
            new List<IFilterMetadata>())
        {
            Exception = new RepositoryException("Repository exception message")
        };

        var filter = new ExceptionMiddleware.ExceptionFilter();

        filter.OnException(context);

        var result = context.Result as ObjectResult;
        
        Assert.IsNotNull(result);
        Assert.AreEqual(404, result.StatusCode);
    }
    
    [TestMethod]
    public void OnException_ServiceException_Returns400()
    {
        var actionContext = new ActionContext(
            new DefaultHttpContext(),
            new RouteData(),
            new ActionDescriptor());

        var context = new ExceptionContext(
            actionContext,
            new List<IFilterMetadata>())
        {
            Exception = new ServiceException("Service exception message")
        };

        var filter = new ExceptionMiddleware.ExceptionFilter();

        filter.OnException(context);

        var result = context.Result as ObjectResult;
        
        Assert.IsNotNull(result);
        Assert.AreEqual(400, result.StatusCode);
    }

}