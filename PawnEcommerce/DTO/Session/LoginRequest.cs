using System;
using System.Diagnostics.CodeAnalysis;

namespace PawnEcommerce.DTO
{
    [ExcludeFromCodeCoverage]
    public class LoginRequest
	{
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

