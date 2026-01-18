using Account.Core.MovementAggregate.Enumerators;
using FluentValidation;
using System;

namespace Account.Application.Movement.Commands.CreateMovement;

public class CreateMovementCommandValidator : AbstractValidator<CreateMovementCommand>
{
    public CreateMovementCommandValidator()
    {
        RuleFor(x => x.RequestId)
            .Must(BeValidId)
            .WithErrorCode("INVALID_REQUEST_ID");

        RuleFor(x => x.AccountNumber)
            .GreaterThan(0)
            .WithErrorCode("INVALID_ACCOUNT_NUMBER");

        RuleFor(x => x.Amount)
            .GreaterThan(0)
            .WithErrorCode("INVALID_VALUE");

        RuleFor(x => x.MovementType)
            .IsInEnum()
            .WithErrorCode("INVALID_TYPE");
    }

    private bool BeValidId(string id)
        => Guid.TryParse(id, out _);

    private bool BePresentInEnum(string movementType)
        => Enum.IsDefined(typeof(MovementTypeEnum), movementType);
}