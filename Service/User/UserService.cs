using System.ComponentModel.Design;
using BCrypt.Net;
using Service.Exception;

namespace Service.User;

public class UserService
{
    private readonly IUserRepository _userRepository;
    
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public void SignUp(IUser user)
    {
        if (Exists(user.Email))
            throw new RepositoryException("User already exists");
        _userRepository.Add(user);
    }

    public IUser? LogIn(string email, string password)
    {
        var toCheckUser = _userRepository.Get(email);
        
        if (toCheckUser is null)
            throw new RepositoryException("User does not exists");
        
        if (!CheckPassword(toCheckUser, password))
            throw new RepositoryException("Invalid credentials");

        return toCheckUser;
    }

    public void DeleteUser(IUser user)
    {
        var toCheckUser = _userRepository.Get(user.Email);
        
        if (toCheckUser is null)
            throw new RepositoryException("User was already deleted");
    }

    private bool Exists(string email)
    {
        var toCheckUser = _userRepository.Get(email);
        return toCheckUser != null;
    }
    
    private bool CheckPassword(IUser user, string password)
    {
        return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
    }
}