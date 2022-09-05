using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Models.Contexts;
using WebApplication4.Models.Entities;
using WebApplication4.Models.Enums;
using WebApplication4.ViewModels.Consulta;

namespace WebApplication4.Controllers
{
    public class ConsultasController : Controller
    {
        private readonly SisMedContext _context;
        private readonly IValidator<AdicionarConsultaViewModel> _adicionarConsultaValidator;
        private readonly IValidator<EditarConsultaViewModel> _editarConsultaValidator;
        private const int TAMANHO_PAGINA = 10;

        public ConsultasController(SisMedContext context, IValidator<AdicionarConsultaViewModel> adicionarConsultaValidator, IValidator<EditarConsultaViewModel> editarConsultaValidator)
        {
            _context = context;
            _adicionarConsultaValidator = adicionarConsultaValidator;
            _editarConsultaValidator = editarConsultaValidator;
        }

        public IActionResult Index(string filtro, int pagina = 1)
        {
            ViewBag.Filtro = filtro;

            var consultas = new List<ListarConsultaViewModel>();
            var query = _context.Consultas.Include(c => c.Paciente)
                                          .Include(c => c.Medico)
                                          .AsQueryable();

            if (!String.IsNullOrWhiteSpace(filtro))
            {
                query = query.Where(c => c.Paciente!.Nome.ToUpper().Contains(filtro.ToUpper()) || c.Medico!.Nome.ToUpper().Contains(filtro.ToUpper()));
            }
            else
            {
                query = query.OrderByDescending(c => c.Id)
                             .Take(10);
            }

            consultas = query.Select(c => new ListarConsultaViewModel
            {
                Id = c.Id,
                Paciente = c.Paciente!.Nome,
                Medico = c.Medico!.Nome,
                Data = c.Data,
                Tipo = c.Tipo == TipoConsulta.Eletiva ? "Eletiva" : "Urgência"
            })
                              .ToList();

            ViewBag.NumeroPagina = pagina;
            ViewBag.TotalPaginas = Math.Ceiling((decimal)consultas.Count() / TAMANHO_PAGINA);
            return View(consultas.Skip((pagina - 1) * TAMANHO_PAGINA)
                                 .Take(TAMANHO_PAGINA)
                                 .ToList());
        }

        // GET: ConsultasController/Adicionar
        public ActionResult Adicionar()
        {
            ViewBag.Medicos = _context.Medicos.OrderBy(m => m.Nome).Select(m => new SelectListItem { Text = m.Nome, Value = m.Id.ToString() });
            ViewBag.TiposConsulta = new[] {
                new SelectListItem { Text = "Eletiva", Value = TipoConsulta.Eletiva.ToString() },
                new SelectListItem { Text = "Urgência", Value = TipoConsulta.Urgencia.ToString() }
            };
            return View();
        }

        // POST: ConsultasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Adicionar(AdicionarConsultaViewModel dados)
        {
            var validacao = _adicionarConsultaValidator.Validate(dados);

            if (!validacao.IsValid)
            {
                ViewBag.Medicos = _context.Medicos.OrderBy(m => m.Nome).Select(m => new SelectListItem { Text = m.Nome, Value = m.Id.ToString() });
                validacao.AddToModelState(ModelState, "");
                return View(dados);
            }

            var consulta = new Consulta
            {
                Data = dados.Data,
                IdMedico = dados.IdMedico,
                IdPaciente = dados.IdPaciente,
                Tipo = dados.Tipo
            };

            _context.Consultas.Add(consulta);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public ActionResult Editar(int id)
        {
            var consulta = _context.Consultas.Include(c => c.Paciente)
                                             .Include(c => c.Medico)
                                             .FirstOrDefault(c => c.Id == id);

            if (consulta != null)
            {
                ViewBag.Medicos = _context.Medicos.OrderBy(m => m.Nome).Select(m => new SelectListItem { Text = m.Nome, Value = m.Id.ToString() });
                ViewBag.TiposConsulta = new[] {
                    new SelectListItem { Text = "Eletiva", Value = TipoConsulta.Eletiva.ToString() },
                    new SelectListItem { Text = "Urgência", Value = TipoConsulta.Urgencia.ToString() }
                };
                return View(new EditarConsultaViewModel
                {
                    IdMedico = consulta.IdMedico,
                    IdPaciente = consulta.IdPaciente,
                    NomePaciente = consulta.Paciente!.Nome,
                    Data = consulta.Data,
                    Tipo = consulta.Tipo
                });
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(int id, EditarConsultaViewModel dados)
        {
            var validacao = _editarConsultaValidator.Validate(dados);

            if (!validacao.IsValid)
            {
                ViewBag.Medicos = _context.Medicos.OrderBy(m => m.Nome).Select(m => new SelectListItem { Text = m.Nome, Value = m.Id.ToString() });
                ViewBag.TiposConsulta = new[] {
                    new SelectListItem { Text = "Eletiva", Value = TipoConsulta.Eletiva.ToString() },
                    new SelectListItem { Text = "Urgência", Value = TipoConsulta.Urgencia.ToString() }
                };
                validacao.AddToModelState(ModelState, "");
                return View(dados);
            }

            var consulta = _context.Consultas.Find(id);

            if (consulta != null)
            {
                consulta.IdPaciente = dados.IdPaciente;
                consulta.IdMedico = dados.IdMedico;
                consulta.Data = dados.Data;
                consulta.Tipo = dados.Tipo;

                _context.Consultas.Update(consulta);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return NotFound();
        }
    }
}
