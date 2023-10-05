using System;
using System.Diagnostics.CodeAnalysis;

namespace PawnEcommerce.Middlewares
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]

    [ExcludeFromCodeCoverage]
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

