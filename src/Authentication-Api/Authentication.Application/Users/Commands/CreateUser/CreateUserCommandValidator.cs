using FluentValidation;

namespace Authentication.Application.Users.Commands.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Cpf)
            .NotEmpty().WithErrorCode("CPF_MUST_BE_INFORMED")
            .NotNull().WithErrorCode("CPF_IS_REQUIRED")
            .EmailAddress().WithErrorCode("CPF_IS_INVALID");

        RuleFor(x => x.Password)
            .NotEmpty().WithErrorCode("PASSWORD_MUST_BE_INFORMED")
            .NotNull().WithErrorCode("PASSWORD_IS_REQUIRED")
            .Length(6, 15).WithErrorCode("PASSWORD_INVALID_LENGTH");

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithErrorCode("CONFIRM_PASSWORD_MUST_BE_INFORMED")
            .NotNull().WithErrorCode("CONFIRM_PASSWORD_IS_REQUIRED")
            .Length(6, 15).WithErrorCode("CONFIRM_PASSWORD_INVALID_LENGTH");
    }
}