using System;
using System.Threading.Tasks;
using Service.Session;
using Service.User;
using Service.User.Role;

namespace PawnEcommerce.Middlewares
{
	public class AuthorizationMiddleware
	{
        private readonly RequestDelegate _next;
        private readonly IServiceProvider _serviceProvider;

        public AuthorizationMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
        {
            _next = next;
            _serviceProvider = serviceProvider;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var endpoint = context.GetEndpoint();
            var attribute = endpoint?.Metadata.GetMetadata<AuthorizationAttribute>();
            if (attribute != null)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var sessionService = scope.ServiceProvider.GetRequiredService<ISessionService>();

                    var token = ExtractTokenFromHeader(context);

                    if (string.IsNullOrEmpty(token))
                    {
                        await RespondUnauthorized(context, "Unauthorized - Token is missing");
                        return;
                    }
                    var user = sessionService.GetCurrentUser(token);

                    if (user == null)
                    {
                        await RespondUnauthorized(context, "Unauthorized - Token not valid");
                        return;
                    }

                    if (!UserHasNecessaryRole(context, user, attribute))
                    {
                        await RespondUnauthorized(context, "Unauthorized - User has no access");
                        return;
                    }

                }
            }
            await _next(context);
        }

        private string ExtractTokenFromHeader(HttpContext context)
        {
            return context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        }

        private async Task RespondUnauthorized(HttpContext context, string message)
        {

            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "text/plain; charset=utf-8";
            await context.Response.WriteAsync(message);
        }

        private bool UserHasNecessaryRole(HttpContext context, User user, AuthorizationAttribute attribute)
        {
            if (Enum.TryParse(typeof(RoleType), attribute.RoleNeeded, out var role))
            {
                return user.Roles.Contains((RoleType)role);
            }
            return false;
        }
    }
}

