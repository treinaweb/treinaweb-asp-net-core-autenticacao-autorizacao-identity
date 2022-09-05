using FluentValidation;
using WebApplication4.ViewModels.MonitoramentoPaciente;

namespace WebApplication4.Validators.MonitoramentoPaciente
{
    public class AdicionarMonitoramentoPacienteValidator:AbstractValidator<AdicionarMonitoramentoViewModel>
    {
        public AdicionarMonitoramentoPacienteValidator()
        {
            RuleFor(x => x.PressaoArterial).NotEmpty().WithMessage("Campo obrigatório");

            RuleFor(x => x.SaturacaoOxigenio).NotEmpty().WithMessage("Campo obrigatório")
                                             .Must(spo2 => spo2 >= 0 && spo2 <= 100).WithMessage("A saturação de oxigênio deve ser um valor entre 0 e 100");

            RuleFor(x => x.Temperatura).NotEmpty().WithMessage("Campo obrigatório")
                                       .Must(temperatura => temperatura > 0).WithMessage("A temperatura não pode ser negativa");
            
            RuleFor(x => x.FrequenciaCardiaca).NotEmpty().WithMessage("Campo obrigatório")
                                              .Must(bpm => bpm > 0).WithMessage("A frequência cardíaca não pode ser negativa");

            RuleFor(x => x.DataAfericao).NotEmpty().WithMessage("Campo obrigatório")
                                        .Must(data => data <= DateTime.Now).WithMessage("A data de aferição não pode ser futura.");

        }
    }
}
