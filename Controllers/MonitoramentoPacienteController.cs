using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.Models.Contexts;
using WebApplication4.Models.Entities;
using WebApplication4.ViewModels.MonitoramentoPaciente;

namespace WebApplication4.Controllers
{
    [Route("Monitoramento")]
    public class MonitoramentoPacienteController : Controller
    {
        private readonly SisMedContext _context;
        private readonly IValidator<AdicionarMonitoramentoViewModel> _adicionarMonitoramentoViewModel;
        private readonly IValidator<EditarMonitoramentoViewModel> _editarMonitoramentoViewModel;

        public MonitoramentoPacienteController(SisMedContext context, IValidator<AdicionarMonitoramentoViewModel> adicionarMonitoramentoViewModel, IValidator<EditarMonitoramentoViewModel> editarMonitoramentoViewModel)
        {
            _context = context;
            _adicionarMonitoramentoViewModel = adicionarMonitoramentoViewModel;
            _editarMonitoramentoViewModel = editarMonitoramentoViewModel;
        }

        // GET: PacientesController
        public ActionResult Index(int idPaciente)
        {
            ViewBag.IdPaciente = idPaciente;

            var monitoramentos = _context.MonitoramentosPaciente.Where(m => m.IdPaciente == idPaciente)
                                         .Select(m => new ListarMonitoramentoViewModel
                                         {
                                             Id = m.Id,
                                             PressaoArterial = m.PressaoArterial,
                                             SaturacaoOxigenio = m.SaturacaoOxigenio,  
                                             FrequenciaCardiaca = m.FrequenciaCardiaca,
                                             Temperatura = m.Temperatura,
                                             DataAfericao = m.DataAfericao
                                         })
                                         .ToList();

            return View(monitoramentos);
        }

        [Route("Adicionar")]
        public ActionResult Adicionar(int idPaciente)
        {
            ViewBag.IdPaciente = idPaciente;
            return View(new AdicionarMonitoramentoViewModel());
        }

        // POST: PacientesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Adicionar")]
        public ActionResult Adicionar(AdicionarMonitoramentoViewModel dados)
        {
            var validacao = _adicionarMonitoramentoViewModel.Validate(dados);

            if (!validacao.IsValid)
            {
                validacao.AddToModelState(ModelState, "");
                return View(dados);
            }
            
            try
            {
                var monitoramento = new MonitoramentoPaciente
                {
                    IdPaciente = dados.IdPaciente,
                    Temperatura = dados.Temperatura,
                    FrequenciaCardiaca = dados.FrequenciaCardiaca,
                    SaturacaoOxigenio = dados.SaturacaoOxigenio,
                    PressaoArterial = dados.PressaoArterial,
                    DataAfericao = dados.DataAfericao
                };

                _context.MonitoramentosPaciente.Add(monitoramento);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index), new { IdPaciente = dados.IdPaciente });
            }
            catch
            {
                return View();
            }
        }

        // GET: PacientesController/Details/5
        [Route("Editar/{id}")]
        public ActionResult Editar(int id)
        {
            var monitoramento = _context.MonitoramentosPaciente.Find(id);

            if (monitoramento != null)
            {
                return View(new EditarMonitoramentoViewModel
                {
                    Id = id,
                    DataAfericao = monitoramento.DataAfericao,  
                    Temperatura = monitoramento.Temperatura,
                    PressaoArterial = monitoramento.PressaoArterial,    
                    FrequenciaCardiaca = monitoramento.FrequenciaCardiaca,
                    SaturacaoOxigenio = monitoramento.SaturacaoOxigenio
                });
            }
            else
            {
                return NotFound();
            }
        }

        // POST: PacientesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Editar/{id}")]
        public ActionResult Editar(int id, EditarMonitoramentoViewModel dados)
        {
            var validacao = _editarMonitoramentoViewModel.Validate(dados);

            if (!validacao.IsValid)
            {
                validacao.AddToModelState(ModelState, "");
                return View(dados);
            }

            try
            {
                var monitoramento = _context.MonitoramentosPaciente.Find(id);

                if(monitoramento != null)
                {
                    monitoramento.Temperatura = dados.Temperatura;
                    monitoramento.SaturacaoOxigenio = dados.SaturacaoOxigenio;
                    monitoramento.FrequenciaCardiaca = dados.FrequenciaCardiaca;
                    monitoramento.DataAfericao = dados.DataAfericao;
                    monitoramento.SaturacaoOxigenio = dados.SaturacaoOxigenio;
                    monitoramento.PressaoArterial = dados.PressaoArterial;

                    _context.MonitoramentosPaciente.Update(monitoramento);
                    _context.SaveChanges();

                    return RedirectToAction(nameof(Index), new { IdPaciente = monitoramento.IdPaciente });
                }
                else return NotFound();
            }
            catch
            {
                return View();
            }
        }

        [Route("Excluir/{id}")]
        public ActionResult Excluir(int id)
        {
            var monitoramento = _context.MonitoramentosPaciente.Find(id);

            if (monitoramento!= null)
            {
                return View(new EditarMonitoramentoViewModel
                {
                    DataAfericao = monitoramento.DataAfericao,
                    Temperatura = monitoramento.Temperatura,
                    PressaoArterial = monitoramento.PressaoArterial,
                    FrequenciaCardiaca = monitoramento.FrequenciaCardiaca,
                    SaturacaoOxigenio = monitoramento.SaturacaoOxigenio
                });
            }
            else
            {
                return NotFound();
            }
        }

        // POST: PacientesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Excluir/{id}")]

        public ActionResult Excluir(int id, IFormCollection collection)
        {
            try
            {
                var monitoramento = _context.MonitoramentosPaciente.Find(id);
               
               if(monitoramento != null)
               {   
                    _context.MonitoramentosPaciente.Remove(monitoramento);

                    _context.SaveChanges();

                    return RedirectToAction(nameof(Index), new { IdPaciente = monitoramento.IdPaciente });
               }
               else return NotFound();
            }
            catch
            {
                return View();
            }
        }
    }
}
