namespace WebApplication4.ViewModels.Auth;

public class LoginViewModel
{
    public string Username { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
    public bool Lembrar { get; set; } = false;
}