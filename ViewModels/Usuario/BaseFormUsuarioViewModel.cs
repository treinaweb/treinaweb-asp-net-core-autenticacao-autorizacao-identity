using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication4.ViewModels.Usuario;

public class BaseFormUsuarioViewModel
{
    public string Username { get; set; } = string.Empty;

    [DisplayName("E-mail")]
    public string Email { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;

    public List<SelectListItem> Roles { get; set; } = new List<SelectListItem>()
    {
        new SelectListItem() { Value = "Admin", Text = "Admin" },
        new SelectListItem() { Value = "User", Text = "User" }
    };
}