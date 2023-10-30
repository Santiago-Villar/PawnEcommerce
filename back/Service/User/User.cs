using System.ComponentModel.DataAnnotations;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using Service.Exception;
using Service.User.Role;
using Service.Sale;

namespace Service.User;

public class User : IUser
{
    [Key]
    public int Id { get; set; }
    public string Address { get; set; }
    public List<RoleType> Roles { get; set; } = new();
    
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

    public ICollection<Service.Sale.Sale> Sales { get; set; }


    public void AddRole(RoleType role)
    {
        Roles.Add(role);
    }
    
    public void RemoveRole(RoleType role)
    {
        Roles.RemoveAll(r => r == role);
    }
    
    private bool IsValidEmail(string email)
    {
        const string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        return Regex.IsMatch(email, pattern);
    }
}


