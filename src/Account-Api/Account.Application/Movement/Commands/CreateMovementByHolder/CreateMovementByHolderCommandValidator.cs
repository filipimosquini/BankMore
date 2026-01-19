using FluentValidation;

namespace Account.Application.Movement.Commands.CreateMovementByHolder;

public class CreateMovementByHolderCommandValidator : AbstractValidator<CreateMovementByHolderCommand>
{
    public CreateMovementByHolderCommandValidator()
    {
        RuleFor(x => x.RequestId)
            .NotEmpty().WithErrorCode("INVALID_REQUEST_ID")
            .NotNull().WithErrorCode("INVALID_REQUEST_ID");

        RuleFor(x => x.Amount)
            .GreaterThan(0)
            .WithErrorCode("INVALID_VALUE");

        RuleFor(x => x.MovementType)
            .IsInEnum()
            .WithErrorCode("INVALID_TYPE");

        RuleFor(x => x.UserId)
            .NotEmpty().WithErrorCode("USER_IS_REQUIRED")
            .NotNull().WithErrorCode("USER_IS_REQUIRED");
    }
}