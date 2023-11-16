namespace Service.User;

public interface IUserRepository
{
    public void Add(User user);
    public User? Get(int id);
    public List<User> GetAll();
    public User? Get(string email);
    public void Delete(User user);
    public User Update(User user);
}