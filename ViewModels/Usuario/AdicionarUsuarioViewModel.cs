using System.ComponentModel;

namespace WebApplication4.ViewModels.Usuario;

public class AdicionarUsuarioViewModel
{
    public string Username { get; set; } = string.Empty;

    [DisplayName("E-mail")]
    public string Email { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;

    [DisplayName("Confirmação senha")]
    public string ConfirmacaoSenha { get; set; } = string.Empty;
}