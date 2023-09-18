using System.Net.Sockets;
using System.Text.RegularExpressions;
using Service.Exception;

namespace Service.User;

public class User
{
    public string Address { get; set; }

    private string _email;
    private string _passwordHash;
    public string PasswordHash
    {
        get => _passwordHash;
        init
        {
            const int saltFactor = 12;
            
            var salt = BCrypt.Net.BCrypt.GenerateSalt(saltFactor);
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(value, salt);
            
            _passwordHash = hashedPassword;
        }
    }
    
    public string Email
    {
        get => _email;
        set
        {
            if (IsValidEmail(value))
            {
                _email = value;
            }
            else
            {
                throw new ModelException("Invalid email address");
            }
        }
    }
    
    private bool IsValidEmail(string email)
    {
        const string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        return Regex.IsMatch(email, pattern);
    }
}


