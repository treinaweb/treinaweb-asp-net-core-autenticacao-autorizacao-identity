using FluentValidation;
using WebApplication4.Models.Contexts;
using WebApplication4.ViewModels.Medico;

namespace WebApplication4.Validators.Medico
{
    public class AdicionarMedicoValidator:AbstractValidator<AdicionarMedicoViewModel>
    {
        public AdicionarMedicoValidator(SisMedContext context)
        {
            RuleFor(x => x.CRM).NotEmpty().WithMessage("Campo obrigatório")
                               .Must(crm => !context.Medicos.Any(m => m.CRM == crm)).WithMessage("Este CRM já está cadastrado.");

            RuleFor(x => x.Nome).NotEmpty().WithMessage("Campo obrigatório")
                                .MaximumLength(100).WithMessage("O nome deve ter até {MaxLength} caracteres");
        }
    }
}
