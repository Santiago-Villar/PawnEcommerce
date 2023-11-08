namespace Service.User;
using DTO.User;

public interface IUserService
{
    public void SignUp(User user);
    public List<User> GetAll();
    public void DeleteUser(int id);
    public void UpdateUser(User updatedUser);
    public User UpdateUserUsingDTO(int id, UserUpdateModel updatedUser);
}