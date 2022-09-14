using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.ViewModels.Auth;

namespace WebApplication4.Controllers;

public class AuthController : Controller
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;

    public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel dados, string? returnUrl)
    {
        if (!ModelState.IsValid)
        {
            return View(dados);
        }
        var result = await _signInManager.PasswordSignInAsync(dados.Username, dados.Senha, dados.Lembrar, false);
        if (!result.Succeeded)
        {
            ModelState.AddModelError(string.Empty, "Usuário ou senha inválidos");
            return View(dados);
        }
        if (returnUrl is not null)
        {
            return LocalRedirect(returnUrl);
        }
        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction(nameof(Login));
    }

    [Authorize]
    public IActionResult AlterarSenha()
    {
        return View();
    }

    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AlterarSenha(AlterarSenhaViewModel dados)
    {
        if (!ModelState.IsValid)
        {
            return View(dados);
        }
        var user = await _userManager.GetUserAsync(User);
        var result = await _userManager.ChangePasswordAsync(user, dados.SenhaAtual, dados.NovaSenha);
        if (!result.Succeeded)
        {
            result.Errors.ToList().ForEach(x => ModelState.AddModelError(string.Empty, x.Description));
            return View(dados);
        }
        await _signInManager.RefreshSignInAsync(user);
        return RedirectToAction(nameof(AlterarSenha));
    }
}