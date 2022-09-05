using FluentValidation;
using System.Text.RegularExpressions;
using WebApplication4.Models.Contexts;
using WebApplication4.ViewModels.Medico;

namespace WebApplication4.Validators.Medico
{
    public class EditarMedicoValidator:AbstractValidator<EditarMedicoViewModel>
    {
        public EditarMedicoValidator(SisMedContext context)
        {
            RuleFor(x => x.CRM).NotEmpty().WithMessage("Campo obrigatório");

            RuleFor(x => x.Nome).NotEmpty().WithMessage("Campo obrigatório")
                                .MaximumLength(100).WithMessage("O nome deve ter até {MaxLength} caracteres");

            RuleFor(x => x).Must(x => !context.Medicos.Any(medico => medico.CRM == x.CRM && medico.Id != x.Id)).WithMessage("Este CRM já está cadastrado.");
        }
    }
}
