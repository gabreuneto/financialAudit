public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Cnpj { get; set; }
    public string Password { get; set; }
    public decimal Balance { get; set; }
    public List<Transaction> Transactions { get; set; } = new List<Transaction>();
    public int UserId { get; internal set; }
}

public interface IUserService
{
    void CreateUser(User newUser);
    IEnumerable<User> GetAllUsers();
    User GetUserByIdOrName(string userId);
    void UpdateUser(int userId, User updatedUser);
    void DeleteUser(int userId);
}
