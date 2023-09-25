using System.ComponentModel.Design;
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
        if (Exists(user))
            throw new RepositoryException("User already exists");
        _userRepository.Add(user);
    }

    public void LogIn(IUser user)
    {
        if (!Exists(user))
            throw new RepositoryException("User does not exists");
        
        var toCheckUser = _userRepository.Get(user.Email);
        if(toCheckUser.PasswordHash != user.PasswordHash)
            throw new RepositoryException("Invalid credentials");
    }

    private bool Exists(IUser user)
    {
        return _userRepository.Exists(user);
    }
}