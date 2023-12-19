using System.Data;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using dbUsers;

public class UserService : IUserService
{
    private List<User> _users; // This is a temporary list; replace it with a real data source.
    private DataTable _userDataTable;

    public UserService()
    {
        // Initialize the list or real data source.
        _users = new List<User>();

        UserData userData = new UserData();
        _userDataTable = userData.CreateUserDataTable();
    }

    public string HashPassword(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }

    public void CreateUser(User newUser)
    {
        DataRow newRow = _userDataTable.NewRow();

        newRow["Name"] = newUser.Name;
        newRow["Cnpj"] = newUser.Cnpj;
        newRow["Password"] = new UserService().HashPassword(newUser.Password);

        _userDataTable.Rows.Add(newRow);
    }

    public IEnumerable<User> GetAllUsers()
    {
        List<User> users = new List<User>();

        foreach (DataRow row in _userDataTable.Rows)
        {
            User user = new User
            {
                UserId = Convert.ToInt32(row["UserId"]),
                Name = (row["Name"] as string) ?? string.Empty,
                Cnpj = (row["Cnpj"] as string) ?? string.Empty,
                Password = (row["Password"] as string) ?? string.Empty
            };

            users.Add(user);
        }

        return users;
    }

    public User GetUserByIdOrName(string identifier)
{
    DataRow[] dr;

    // Verifica se o identificador é um número (assumindo que UserId é sempre um número)
    if (int.TryParse(identifier, out int userId))
    {
        // Se for um número, procura pelo UserId
        dr = _userDataTable.Select($@"UserId = {userId}");
    }
    else
    {
        // Se não for um número, procura pelo Name
        dr = _userDataTable.Select($@"Name = '{identifier}'");
    }

    if (dr.Length > 0)
    {
        string name = (dr[0]["Name"] as string) ?? string.Empty;
        string cnpj = (dr[0]["Cnpj"] as string) ?? string.Empty;
        string password = (dr[0]["Password"] as string) ?? string.Empty;
        userId = Convert.ToInt32(dr[0]["UserId"]);

        User user = new User
        {
            Name = name,
            Cnpj = cnpj,
            Password = password,
            Transactions = new List<Transaction>(),
            UserId = userId
        };

        return user;
    }
    else
    {
        throw new InvalidOperationException("User not found");
    }
}


    public void UpdateUser(int userId, User updatedUser)
    {
        // Find the row corresponding to the user by UserId
        DataRow[] dr = _userDataTable.Select($"UserId = {userId}");

        if (dr.Length > 0)
        {
            // Update values in the found row
            dr[0]["Name"] = updatedUser.Name;
            dr[0]["Cnpj"] = updatedUser.Cnpj;
            dr[0]["Password"] = updatedUser.Password;

            // Save changes to the DataTable
            _userDataTable.AcceptChanges();
        }
        else
        {
            throw new InvalidOperationException("User not found");
        }
    }

    public void DeleteUser(int userId)
    {
        // Find the row corresponding to the user by UserId
        DataRow[] dr = _userDataTable.Select($"UserId = {userId}");

        if (dr.Length > 0)
        {
            // Delete the found row
            dr[0].Delete();

            // Save changes to the DataTable for history, as requested
            _userDataTable.AcceptChanges();
        }
        else
        {
            throw new InvalidOperationException("User not found");
        }
    }
}
