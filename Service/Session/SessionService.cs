using System;
using Service.Exception;
using Service.User;
using BCrypt.Net;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;


namespace Service.Session
{
	public class SessionService : ISessionService
	{
        private string SecretKey = "asldasdkLDKSALKD32DK2O3KDASKDslakDLSAKF";
        public IUserRepository _repository { get; set; }

		public SessionService(IUserRepository repository)
		{
            _repository = repository;

		}

        public string Authenticate(string email, string password)
        {
            IUser user = _repository.Get(email) ?? throw new RepositoryException("User does not exists");
            if(!VerifyPassword(password,user.PasswordHash))
            {
                throw new RepositoryException("Invalid credentials");
            }
            return GetToken(user);
        }


        private Boolean VerifyPassword(string password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }

        private String GetToken(IUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                }),
                Expires = DateTime.UtcNow.AddDays(1), // El token expira en 1 día
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}

