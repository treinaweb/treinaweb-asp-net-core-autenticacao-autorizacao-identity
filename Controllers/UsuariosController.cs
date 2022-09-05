using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.ViewModels.Usuario;

namespace WebApplication4.Controllers;

public class UsuariosController : Controller
{
    private readonly IValidator<AdicionarUsuarioViewModel> _adicionarUsuarioValidator;
    private readonly UserManager<IdentityUser> _userManager;

    public UsuariosController(
        IValidator<AdicionarUsuarioViewModel> adicionarUsuarioValidator,
        UserManager<IdentityUser> userManager)
    {
        _adicionarUsuarioValidator = adicionarUsuarioValidator;
        _userManager = userManager;
    }

    public IActionResult Adicionar()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Adicionar(AdicionarUsuarioViewModel dados)
    {
        var validacaoResult = _adicionarUsuarioValidator.Validate(dados);
        if (!validacaoResult.IsValid)
        {
            validacaoResult.AddToModelState(ModelState, string.Empty);
            return View(dados);
        }
        var user = new IdentityUser
        {
            UserName = dados.Username,
            Email = dados.Email,
            PhoneNumber = dados.Telefone
        };
        var identityResult = await _userManager.CreateAsync(user, dados.Senha);
        if (!identityResult.Succeeded)
        {
            identityResult.Errors.ToList()
                .ForEach(e => ModelState.AddModelError(string.Empty, e.Description));
            return View(dados);
        }
        return RedirectToAction(nameof(Adicionar));
    }
}