using System;
using System.Security.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Service.Exception;

namespace PawnEcommerce.Middlewares;
public class ExceptionMiddleware : Attribute, IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var (statusCode, errorMessage) = GetExceptionData(context.Exception);
        var result = new ObjectResult(new { Message = errorMessage })
        {
            StatusCode = statusCode
        };
        context.Result = result;
    }

    private (int statusCode, string errorMessage) GetExceptionData(Exception exception)
    {
        return exception switch
        {
            RepositoryException => (404, exception.Message),
            ServiceException or ModelException => (400, exception.Message),
            InvalidCredentialException => (401, exception.Message),
            _ => (500, "Sorry, try again later")
        };
    }
}
