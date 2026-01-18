using Caelum.Stella.CSharp.Validation;
using FluentValidation;

namespace Account.Application.Account.Commands.CreateAccount;

public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
{
    public CreateAccountCommandValidator()
    {
        RuleFor(x => x.Cpf)
            .NotEmpty().WithErrorCode("DOCUMENT_MUST_BE_INFORMED")
            .NotNull().WithErrorCode("DOCUMENT_IS_REQUIRED")
            .Must(ValidDocument).WithErrorCode("DOCUMENT_IS_INVALID");

        RuleFor(x => x.UserId)
            .NotEmpty().WithErrorCode("USER_IS_REQUIRED")
            .NotNull().WithErrorCode("USER_IS_REQUIRED");
    }

    private bool ValidDocument(string cpf)
        => new CPFValidator().IsValid(cpf);
}