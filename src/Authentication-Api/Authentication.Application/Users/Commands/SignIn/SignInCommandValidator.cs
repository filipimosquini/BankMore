using FluentValidation;

namespace Authentication.Application.Users.Commands.SignIn;

public class SignInCommandValidator : AbstractValidator<SignInCommand>
{
    public SignInCommandValidator()
    {
        RuleFor(x => x.Cpf)
            .NotEmpty().WithErrorCode("CPF_MUST_BE_INFORMED")
            .NotNull().WithErrorCode("CPF_IS_REQUIRED")
            .EmailAddress().WithErrorCode("CPF_IS_INVALID");

        RuleFor(x => x.Password)
            .NotEmpty().WithErrorCode("PASSWORD_MUST_BE_INFORMED")
            .NotNull().WithErrorCode("PASSWORD_IS_REQUIRED")
            .Length(6, 15).WithErrorCode("PASSWORD_INVALID_LENGTH");
    }
}