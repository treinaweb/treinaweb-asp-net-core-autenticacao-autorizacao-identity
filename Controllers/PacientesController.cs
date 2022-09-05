using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using WebApplication4.Models.Contexts;
using WebApplication4.Models.Entities;
using WebApplication4.ViewModels.Paciente;

namespace WebApplication4.Controllers
{
    public class PacientesController : Controller
    {
        private readonly SisMedContext _context;
        private readonly IValidator<AdicionarPacienteViewModel> _adicionarPacienteValidator;
        private readonly IValidator<EditarPacienteViewModel> _editarPacienteValidator;
        private const int TAMANHO_PAGINA = 10;
        public PacientesController(SisMedContext context, IValidator<AdicionarPacienteViewModel> adicionarPacienteValidator, IValidator<EditarPacienteViewModel> editarPacienteValidator)
        {
            _context = context;
            _adicionarPacienteValidator = adicionarPacienteValidator;
            _editarPacienteValidator = editarPacienteValidator;
        }

        // GET: PacientesController
        public ActionResult Index(string filtro, int pagina = 1)
        {
            ViewBag.Filtro = filtro;

            var condicao = (Paciente p) => String.IsNullOrWhiteSpace(filtro) || p.Nome.ToUpper().Contains(filtro.ToUpper()) || p.CPF.Contains(filtro.Replace(".", "").Replace("-", ""));

            var pacientes = _context.Pacientes.Where(condicao)
                                              .Select(p => new ListarPacienteViewModel
                                              {
                                                  Id = p.Id,
                                                  Nome = p.Nome,
                                                  CPF = p.CPF
                                              });
                                              

            ViewBag.NumeroPagina = pagina;
            ViewBag.TotalPaginas = Math.Ceiling((decimal)pacientes.Count() / TAMANHO_PAGINA);
            return View(pacientes.Skip((pagina - 1) * TAMANHO_PAGINA)
                                 .Take(TAMANHO_PAGINA)
                                 .ToList());
        }

        // GET: PacientesController/Create
        public ActionResult Adicionar()
        {
            return View();
        }

        // POST: PacientesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Adicionar(AdicionarPacienteViewModel dados)
        {
            var validacao = _adicionarPacienteValidator.Validate(dados);

            if (!validacao.IsValid)
            {
                validacao.AddToModelState(ModelState, "");
                return View(dados);
            }

            var paciente = new Paciente
            {
                CPF = Regex.Replace(dados.CPF, "[^0-9]", ""),
                Nome = dados.Nome,
                DataNascimento = dados.DataNascimento
            };

            _context.Pacientes.Add(paciente);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // GET: PacientesController/Details/5
        public ActionResult Editar(int id)
        {
            var paciente = _context.Pacientes.Find(id);
                                             
            if (paciente != null)
            {
                var informacoesComplementares = _context.InformacoesComplementaresPaciente.FirstOrDefault(i => i.IdPaciente == id);

                return View(new EditarPacienteViewModel
                {
                    Id = paciente.Id,
                    CPF = paciente.CPF,
                    Nome = paciente.Nome,
                    DataNascimento = paciente.DataNascimento,
                    Alergias = informacoesComplementares?.Alergias,
                    MedicamentosEmUso = informacoesComplementares?.MedicamentosEmUso,
                    CirurgiasRealizadas = informacoesComplementares?.CirurgiasRealizadas
                });
            }

            return NotFound();
        }

        // POST: PacientesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(int id, EditarPacienteViewModel dados)
        {
            var validacao = _editarPacienteValidator.Validate(dados);

            if (!validacao.IsValid)
            {
                validacao.AddToModelState(ModelState, "");
                return View(dados);
            }

            var paciente = _context.Pacientes.Find(id);

            if (paciente != null)
            {
                paciente.CPF = Regex.Replace(dados.CPF, "[^0-9]", "");
                paciente.Nome = dados.Nome;
                paciente.DataNascimento = dados.DataNascimento;

                var informacoesComplementares = _context.InformacoesComplementaresPaciente.FirstOrDefault(i => i.IdPaciente == id);

                if (informacoesComplementares == null)
                    informacoesComplementares = new InformacoesComplementaresPaciente();

                informacoesComplementares.Alergias = dados.Alergias;
                informacoesComplementares.MedicamentosEmUso = dados.MedicamentosEmUso;
                informacoesComplementares.CirurgiasRealizadas = dados.CirurgiasRealizadas;
                informacoesComplementares.IdPaciente = id;

                if (informacoesComplementares.Id > 0)
                    _context.InformacoesComplementaresPaciente.Update(informacoesComplementares);
                else
                    _context.InformacoesComplementaresPaciente.Add(informacoesComplementares);

                _context.Pacientes.Update(paciente);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            
            return NotFound();
        }

        // GET: PacientesController/Excluir/5
        public ActionResult Excluir(int id)
        {
            var paciente = _context.Pacientes.Find(id);

            if (paciente != null)
            {
                return View(new EditarPacienteViewModel
                {
                    Id = paciente.Id,
                    CPF = paciente.CPF,
                    Nome = paciente.Nome,
                    DataNascimento = paciente.DataNascimento
                });
            }
            
            return NotFound();
        }

        // POST: PacientesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Excluir(int id, IFormCollection collection)
        {
            var paciente = _context.Pacientes.Find(id);

            if (paciente != null)
            {
                var informacoesComplementares = _context.InformacoesComplementaresPaciente.FirstOrDefault(i => i.IdPaciente == id);

                if (informacoesComplementares != null)
                {
                    _context.InformacoesComplementaresPaciente.Remove(informacoesComplementares);
                }

                _context.Pacientes.Remove(paciente);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            
            return NotFound();
        }

        public ActionResult Buscar(string filtro)
        {
            var pacientes = new List<ListarPacienteViewModel>();

            if (!String.IsNullOrWhiteSpace(filtro))
            {
                pacientes = _context.Pacientes.Where(p => p.Nome.Contains(filtro) || p.CPF.Contains(filtro.Replace(".", "").Replace("-", "")))
                                              .Take(10)
                                              .Select(p => new ListarPacienteViewModel
                                              {
                                                  Id = p.Id,
                                                  Nome = p.Nome,
                                                  CPF = p.CPF
                                              })
                                              .ToList();
            }

            return Json(pacientes);
        }
    }
}
