using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.ViewModels.Auth;

namespace WebApplication4.Controllers;

public class AuthController : Controller
{
    private readonly SignInManager<IdentityUser> _signInManager;

    public AuthController(SignInManager<IdentityUser> signInManager)
    {
        _signInManager = signInManager;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel dados)
    {
        if (!ModelState.IsValid)
        {
            return View(dados);
        }
        var result = await _signInManager.PasswordSignInAsync(dados.Username, dados.Senha, false, false);
        if (!result.Succeeded)
        {
            ModelState.AddModelError(string.Empty, "Usuário ou senha inválidos");
            return View(dados);
        }
        return RedirectToAction("Index", "Home");
    }
}