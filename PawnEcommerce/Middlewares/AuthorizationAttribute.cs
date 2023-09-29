using System;
namespace PawnEcommerce.Middlewares
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizationAttribute : Attribute
	{
        public string RoleNeeded { get; set; }

        public AuthorizationAttribute()
        {
        }

        public AuthorizationAttribute(string roleNeeded)
        {
            RoleNeeded = roleNeeded;
        }
    }
}

