using Agenda.Application.DTOS.InputModels;
using FluentValidation;


namespace Agenda.Application.Validators
{
    public class UpdateContatoInputValidator : AbstractValidator<UpdateContatoInput>
    {
        public UpdateContatoInputValidator()
        {
            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("O e-mail é obrigatório.")
                .EmailAddress().WithMessage("Informe um e-mail válido.");

            RuleFor(c => c.Telefone)
                .NotEmpty().WithMessage("O telefone é obrigatório.")
                .Length(8, 15).WithMessage("O telefone deve ter entre 8 e 15 caracteres.");

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