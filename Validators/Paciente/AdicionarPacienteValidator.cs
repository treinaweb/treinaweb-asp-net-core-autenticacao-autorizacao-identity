using FluentValidation;
using System.Text.RegularExpressions;
using WebApplication4.Models.Contexts;
using WebApplication4.ViewModels.Paciente;

namespace WebApplication4.Validators.Paciente
{
    public class AdicionarPacienteValidator:AbstractValidator<AdicionarPacienteViewModel>
    {
        public AdicionarPacienteValidator(SisMedContext context)
        {
            RuleFor(x => x.CPF).NotEmpty().WithMessage("Campo obrigatório")
                               .Must(cpf => Regex.Replace(cpf, "[^0-9]", "").Length == 11).WithMessage("CPF inválido")
                               .Must(cpf => !context.Pacientes.Any(p => p.CPF == Regex.Replace(cpf, "[^0-9]", ""))).WithMessage("Este CPF já está cadastrado.");

            RuleFor(x => x.Nome).NotEmpty().WithMessage("Campo obrigatório")
                                .MaximumLength(100).WithMessage("O nome deve ter até {MaxLength} caracteres");

            RuleFor(x => x.DataNascimento).NotEmpty().WithMessage("Campo obrigatório")
                                          .Must(data => data <= DateTime.Today).WithMessage("A data de nascimento não pode ser futura.");

        }
    }
}
