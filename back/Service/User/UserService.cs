using System.ComponentModel.Design;
using System.Security.Authentication;
using BCrypt.Net;
using Service.DTO.Product;
using Service.DTO.User;
using Service.Exception;
using Service.Product;

namespace Service.User;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public void SignUp(User user)
    {
        if (Exists(user.Email))
            throw new ServiceException("User already exists");
        
        _userRepository.Add(user);
    }

    public User LogIn(string email, string password)
    {
        var toCheckUser = FindUser(email);
        if (!CheckPassword(toCheckUser, password))
            throw new InvalidCredentialException("Invalid credentials");
        
        return toCheckUser;
    }

    public void DeleteUser(int id)
    {
        var toCheckUser = Get(id);
        _userRepository.Delete(toCheckUser);
    }
    
    public void UpdateUser(User updatedUser)
    {
        var toUpdateUser = Get(updatedUser.Id);
        _userRepository.Update(updatedUser);
    }

    public User UpdateUserUsingDTO(int id, UserUpdateModel userDto)
    {
        var existingUser = _userRepository.Get(id);
        if (userDto.Password != null)
        {
            throw new ServiceException("Password cannot be updated");
        }

        if (userDto.Email != null) existingUser.Email = userDto.Email;
        if (userDto.Address != null) existingUser.Address = userDto.Address;

        return _userRepository.Update(existingUser);
    }


    public User Get(int id)
    {
        var foundUser = _userRepository.Get(id);
        if (foundUser == null)
            throw new RepositoryException("User does not exists");

        return foundUser;
    }

    public List<User> GetAll()
    {
        return _userRepository.GetAll();
    }

    private User FindUser(string email)
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
    
    private bool CheckPassword(User user, string password)
    {
        return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
    }
}