namespace Service.User;

public interface IUserRepository
{
    public void Add(IUser user);
    public bool Exists(IUser user);
}