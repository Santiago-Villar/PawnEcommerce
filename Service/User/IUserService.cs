namespace Service.User;

public interface IUserService
{
    public void SignUp(User user);

    public User LogIn(string email, string password);

    public void DeleteUser(User user);

    public void UpdateUser(User updatedUser);
}