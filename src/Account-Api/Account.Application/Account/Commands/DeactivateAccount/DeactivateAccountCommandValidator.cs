using FluentValidation;

namespace Account.Application.Account.Commands.DeactivateAccount;

public class DeactivateAccountCommandValidator : AbstractValidator<DeactivateAccountCommand>
{
    public DeactivateAccountCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithErrorCode("USER_IS_REQUIRED")
            .NotNull().WithErrorCode("USER_IS_REQUIRED");
    }
}