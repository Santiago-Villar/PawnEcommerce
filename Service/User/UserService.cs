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
        if (Exits(user))
            throw new RepositoryException("User already Exists");
        _userRepository.Add(user);
    }

    private bool Exits(IUser user)
    {
        return _userRepository.Exists(user);
    }
}