namespace Service.User;

public interface IUserService
{
    public void SignUp(IUser user);

    public IUser LogIn(string email, string password);

    public void DeleteUser(IUser user);

    public void UpdateUser(IUser updatedUser);
}