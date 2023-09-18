using System.Text.RegularExpressions;
using Service.Exception;

namespace Service.User;

public class User
{
    private string _email;
    
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


