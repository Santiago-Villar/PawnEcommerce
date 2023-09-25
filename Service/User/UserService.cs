namespace Service.User;

public class UserService
{
    private readonly IUserRepository UserRepository;
    public UserService(IUserRepository userRepository)
    {
        UserRepository = userRepository;
    }
}