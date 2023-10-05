namespace Service.User;

public interface IUserService
{
    public void SignUp(User user);

    public List<User> GetAll();

    public void DeleteUser(int id);

    public void UpdateUser(User updatedUser);
}