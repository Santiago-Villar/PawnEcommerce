using System.ComponentModel.Design;
using BCrypt.Net;
using Service.Exception;

namespace Service.User;

public class UserService : IUserService
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

    public IUser LogIn(string email, string password)
    {
        var toCheckUser = FindUser(email);
        if (!CheckPassword(toCheckUser, password))
            throw new RepositoryException("Invalid credentials");
        
        return toCheckUser;
    }

    public void DeleteUser(IUser user)
    {
        var toCheckUser = Get(user.Id);
        _userRepository.Delete(toCheckUser);
    }
    
    public void UpdateUser(IUser updatedUser)
    {
        var toUpdateUser = Get(updatedUser.Id);
        toUpdateUser.Address = updatedUser.Address;
        _userRepository.Update(toUpdateUser);
    }
    
    public IUser Get(int id)
    {
        var foundUser = _userRepository.Get(id);
        if (foundUser == null)
            throw new RepositoryException("User does not exists");

        return foundUser;
    }

    private IUser FindUser(string email)
    {
        var foundUser = _userRepository.Get(email);
        if (foundUser == null)
            throw new RepositoryException("User does not exists");

        return foundUser;
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