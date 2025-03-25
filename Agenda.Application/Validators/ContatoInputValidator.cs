

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


            RuleFor(x => x.Telefone)
            .NotEmpty().WithMessage("Telefone é obrigatório.")
            .Matches(@"^\d{10,11}$").WithMessage("Telefone deve conter apenas números e ter entre 10 e 11 dígitos.");

            RuleFor(c => c.Endereco)
                .NotEmpty().WithMessage("O endereço é obrigatório.")
                .MaximumLength(200).WithMessage("O endereço deve ter no máximo 200 caracteres.");

            RuleFor(c => c.DataNascimento)
                .NotEmpty().WithMessage("A data de nascimento é obrigatória.")
                .LessThan(DateTime.Now).WithMessage("A data de nascimento deve ser no passado.");

            RuleFor(c => c.Site)
                .MaximumLength(100).WithMessage("O site deve ter no máximo 100 caracteres.")
                .When(c => !string.IsNullOrEmpty(c.Site));

            RuleFor(c => c.TelefoneComercial)
                .MaximumLength(15).WithMessage("O telefone comercial deve ter no máximo 15 caracteres.")
                .When(c => !string.IsNullOrEmpty(c.TelefoneComercial));
        }
    }
}
