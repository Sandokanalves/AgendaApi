using Agenda.Application.DTOS.InputModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }
    }
}