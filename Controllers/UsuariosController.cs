using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.ViewModels.Usuario;

namespace WebApplication4.Controllers;

[Authorize(Roles = "Admin")]
public class UsuariosController : Controller
{
    private readonly IValidator<AdicionarUsuarioViewModel> _adicionarUsuarioValidator;
    private readonly IValidator<EditarUsuarioViewModel> _editarUsuarioValidator;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UsuariosController(
        IValidator<AdicionarUsuarioViewModel> adicionarUsuarioValidator,
        UserManager<IdentityUser> userManager,
        IValidator<EditarUsuarioViewModel> editarUsuarioValidator,
        RoleManager<IdentityRole> roleManager)
    {
        _adicionarUsuarioValidator = adicionarUsuarioValidator;
        _userManager = userManager;
        _editarUsuarioValidator = editarUsuarioValidator;
        _roleManager = roleManager;
    }

    public IActionResult Index()
    {
        var usuarios = _userManager.Users.Select(u => new ListaUsuarioViewModel
        {
            Id = u.Id,
            Username = u.UserName,
            Email = u.Email,
            Telefone = u.PhoneNumber
        });
        return View(usuarios);
    }

    public IActionResult Adicionar()
    {
        return View(new AdicionarUsuarioViewModel());
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
        if (!await _roleManager.RoleExistsAsync(dados.Role))
        {
            var role = new IdentityRole(dados.Role);
            await _roleManager.CreateAsync(role);
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
        await _userManager.AddToRoleAsync(user, dados.Role);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Editar(string id)
    {
        var usuario = _userManager.Users.FirstOrDefault(u => u.Id == id);
        if (usuario is null)
        {
            return NotFound();
        }
        var dados = new EditarUsuarioViewModel
        {
            Username = usuario.UserName,
            Email = usuario.Email,
            Telefone = usuario.PhoneNumber,
            Role = (await _userManager.GetRolesAsync(usuario)).FirstOrDefault() ?? string.Empty
        };
        return View(dados);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Editar(string id, EditarUsuarioViewModel dados)
    {
        var validacaoResult = _editarUsuarioValidator.Validate(dados);
        if (!validacaoResult.IsValid)
        {
            validacaoResult.AddToModelState(ModelState, string.Empty);
            return View(dados);
        }
        var usuario = _userManager.Users.FirstOrDefault(u => u.Id == id);
        if (usuario is null)
        {
            return NotFound();
        }
        if (!await _roleManager.RoleExistsAsync(dados.Role))
        {
            var role = new IdentityRole(dados.Role);
            await _roleManager.CreateAsync(role);
        }
        usuario.UserName = dados.Username;
        usuario.Email = dados.Email;
        usuario.PhoneNumber = dados.Telefone;
        var identityResult = await _userManager.UpdateAsync(usuario);
        if (!identityResult.Succeeded)
        {
            identityResult.Errors.ToList()
                .ForEach(e => ModelState.AddModelError(string.Empty, e.Description));
            return View(dados);
        }
        await _userManager.RemoveFromRolesAsync(usuario, await _userManager.GetRolesAsync(usuario));
        await _userManager.AddToRoleAsync(usuario, dados.Role);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Excluir(string id)
    {
        var usuario = _userManager.Users.FirstOrDefault(u => u.Id == id);
        if (usuario is null)
        {
            return NotFound();
        }
        await _userManager.DeleteAsync(usuario);
        return RedirectToAction(nameof(Index));
    }
}