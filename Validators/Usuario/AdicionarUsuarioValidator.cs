using FluentValidation;
using WebApplication4.ViewModels.Usuario;

namespace WebApplication4.Validators.Usuario;

public class AdicionarUsuarioValidator : AbstractValidator<AdicionarUsuarioViewModel>
{
    public AdicionarUsuarioValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
            .MaximumLength(50).WithMessage("O campo {PropertyName} deve ter no máximo {MaxLength} caracteres");
        RuleFor(x => x.Telefone)
            .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
            .Length(11).WithMessage("O campo {PropertyName} deve ter {MaxLength} caracteres")
            .Must(x => x.ToList().All(c => char.IsDigit(c))).WithMessage("O campo {PropertyName} deve conter apenas números");
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
            .EmailAddress().WithMessage("O campo {PropertyName} está em um formato inválido");
        RuleFor(x => x.Senha)
            .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
            .MinimumLength(6).WithMessage("O campo {PropertyName} deve ter no mínimo {MinLength} caracteres");
        RuleFor(x => x.ConfirmacaoSenha)
            .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
            .Equal(x => x.Senha).WithMessage("O campo {PropertyName} deve ser igual ao campo Senha");
    }
}