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
        private readonly ISessionService _sessionService;

        public AuthorizationMiddleware(RequestDelegate next, ISessionService sessionService)
        {
            _next = next;
            _sessionService = sessionService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = ExtractTokenFromHeader(context);

            if (string.IsNullOrEmpty(token))
            {
                await RespondUnauthorized(context, "Unauthorized - Token is missing");
                return;
            }

            var user = _sessionService.GetCurrentUser(token);

            if (user == null)
            {
                await RespondUnauthorized(context, "Unauthorized - Token not valid");
                return;
            }

            if (!UserHasNecessaryRole(context, user))
            {
                await RespondUnauthorized(context, "Unauthorized - User has no access");
                return;
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
            await context.Response.WriteAsync(message);
        }

        private bool UserHasNecessaryRole(HttpContext context, IUser user)
        {
            var endpoint = context.GetEndpoint();
            var attribute = endpoint?.Metadata.GetMetadata<AuthorizationAttribute>();
            if (attribute == null)
            {
                return true;
            }

            if (Enum.TryParse(typeof(RoleType), attribute.RoleNeeded, out var role))
            {
                return user.Roles.Contains((RoleType) role);
            }

            return true;
        }
    }
}

