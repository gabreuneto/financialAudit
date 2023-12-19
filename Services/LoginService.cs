// LoginService.cs
public class LoginService : ILoginService
{
    private readonly IUserService _userService;

    public LoginService(IUserService userService)
    {
        _userService = userService;
    }

    public User ValidateCredentials(string username, string password)
    {
        // Verifica se o usuário com o nome de usuário e senha fornecidos existe
        // Se as credenciais estiverem corretas, retorne o usuário; caso contrário, retorne null
        User user = _userService.GetUserByIdOrName(username);

        if (user != null && ValidatePassword(password, user.Password))
        {
            // Limpe a senha antes de retornar o usuário
            user.Password = null;
            return user;
        }

        return null;
    }

    // Método para validar a senha
    private bool ValidatePassword(string inputPassword, string hashedPassword)
    {
        // Adicione lógica de validação de senha, como comparar hashes
        // Você pode usar a mesma lógica que você usou para criar o hash no UserService
        // Retorne true se as senhas corresponderem; caso contrário, retorne false
        return hashedPassword == new UserService().HashPassword(inputPassword);
    }
}

public interface ILoginService
{
    User ValidateCredentials(string username, string password);
}
