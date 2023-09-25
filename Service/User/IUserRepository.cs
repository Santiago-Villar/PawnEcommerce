namespace Service.User;

public interface IUserRepository
{
    public void Add(IUser user);
    public IUser? Get(string email);
    public void Delete(IUser user);
}