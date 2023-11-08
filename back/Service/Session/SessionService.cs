using System;
using Service.Exception;
using Service.User;
using BCrypt.Net;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using Microsoft.IdentityModel.Tokens;
using System.Net.Http;
using Microsoft.AspNetCore.Http;


namespace Service.Session
{
	public class SessionService : ISessionService
	{
        private string SecretKey = "asldasdkLDKSALKD32DK2O3KDASKDslakDLSAKF";
        public IUserRepository _repository { get; set; }
        private readonly IHttpContextAccessor _httpContextAccessor;

		public SessionService(IUserRepository repository, IHttpContextAccessor httpContextAccessor)
		{
            _httpContextAccessor = httpContextAccessor;
            _repository = repository;
		}

        public string Authenticate(string email, string password)
        {
            User.User user = _repository.Get(email) ?? throw new RepositoryException("User does not exists");
            if(!VerifyPassword(password,user.PasswordHash))
            {
                throw new InvalidCredentialException("Invalid credentials");
            }
            return createToken(user);
        }

        public User.User? GetCurrentUser()
        {
            var token = ExtractTokenFromHeader(_httpContextAccessor.HttpContext);
            var userEmail = ExtractUserEmailFromToken(token);
            if (userEmail == null)
                return null;

            return _repository.Get(userEmail);
        }


        private Boolean VerifyPassword(string password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }

        private String createToken(User.User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(1), // El token expira en 1 día
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private string ExtractUserEmailFromToken(string token)
        {
            if (string.IsNullOrEmpty(token))
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SecretKey);

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                foreach (var claim in jwtToken.Claims)
                {
                    Console.WriteLine($"Type: {claim.Type}, Value: {claim.Value}");
                }
                var userEmailClaim = jwtToken.Claims.First(c => c.Type == "email");
                return userEmailClaim.Value;
            }
            catch(System.Exception ex)
            {
                Console.WriteLine("exception next:");
                Console.WriteLine(ex);
                return null;
            }
        }

        public int? ExtractUserIdFromToken(string token)
        {
            if (string.IsNullOrEmpty(token))
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SecretKey);

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userIdClaim = jwtToken.Claims.First(c => c.Type == "nameid");
                return int.Parse(userIdClaim.Value);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("exception next:");
                Console.WriteLine(ex);
                return null;
            }
        }
        private string ExtractTokenFromHeader(HttpContext context)
        {
            return context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        }

    }
}


