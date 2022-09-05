using Microsoft.AspNetCore.Mvc;

namespace WebApplication4.Controllers;

public class UsuariosController : Controller
{
    public IActionResult Adicionar()
    {
        return View();
    }
}