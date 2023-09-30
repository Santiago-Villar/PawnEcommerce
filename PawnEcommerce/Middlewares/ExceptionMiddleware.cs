using System.Security.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Service.Exception;

namespace PawnEcommerce.Middlewares;

public class ExceptionMiddleware
{
    public class ExceptionFilter : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            try
            {
                throw context.Exception;
            }
            catch (RepositoryException e)
            {
                context.Result = new ObjectResult(new { Message = e.Message }) { StatusCode = 404 };
            }
            catch (ServiceException e)
            {
                context.Result = new ObjectResult(new { Message = e.Message }) { StatusCode = 400 };
            }
        }
    }
}