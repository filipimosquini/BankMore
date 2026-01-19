using FluentValidation;

namespace Account.Application.Account.Commands.CreateAccount;

public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
{
    public CreateAccountCommandValidator()
    {
        RuleFor(x => x.RequestId)
            .NotEmpty().WithErrorCode("INVALID_REQUEST_ID")
            .NotNull().WithErrorCode("INVALID_REQUEST_ID");

        RuleFor(x => x.Holder)
            .NotEmpty().WithErrorCode("HOLDER_MUST_BE_INFORMED")
            .NotNull().WithErrorCode("HOLDER_IS_REQUIRED");

        RuleFor(x => x.UserId)
            .NotEmpty().WithErrorCode("USER_IS_REQUIRED")
            .NotNull().WithErrorCode("USER_IS_REQUIRED");
    }
}