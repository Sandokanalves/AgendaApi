

using Agenda.Application.DTOS.InputModels;
using FluentValidation;

namespace Agenda.Application.Validators
{
    public class ContatoInputValidator : AbstractValidator<CreateContatoInputModel>
    {
        public ContatoInputValidator()
        {
            
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O nome é obrigatório.")
                .MinimumLength(3).WithMessage("O nome deve ter no mínimo 3 caracteres.")
                .MaximumLength(100).WithMessage("O nome deve ter no máximo 100 caracteres.");

            
            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("O e-mail é obrigatório.")
                .EmailAddress().WithMessage("O e-mail deve ser válido.");

           
            RuleFor(c => c.Telefone)
                .NotEmpty().WithMessage("O telefone é obrigatório.")
                .Matches(@"^\(\d{2}\) \d{4,5}-\d{4}$")
                .WithMessage("O telefone deve estar no formato (99) 99999-9999.");
        }
    }
}
