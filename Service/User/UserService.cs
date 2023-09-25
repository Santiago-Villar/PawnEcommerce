using Service.Exception;

namespace Service.User;

public class UserService
{
    private readonly IUserRepository UserRepository;
    public UserService(IUserRepository userRepository)
    {
        UserRepository = userRepository;
    }

    public void SignUp(IUser user)
    {
        if (Exits(user))
            throw new RepositoryException("User already Exists");
        UserRepository.Add(user);
    }

    private bool Exits(IUser user)
    {
        return UserRepository.Exists(user);
    }
}