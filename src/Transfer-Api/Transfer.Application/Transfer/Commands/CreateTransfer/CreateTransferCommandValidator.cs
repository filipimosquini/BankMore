using FluentValidation;

namespace Transfer.Application.Transfer.Commands.CreateTransfer;

public class CreateTransferCommandValidator : AbstractValidator<CreateTransferCommand>
{
    public CreateTransferCommandValidator()
    {
        RuleFor(x => x.RequestId)
            .NotEmpty().WithErrorCode("INVALID_REQUEST_ID")
            .NotNull().WithErrorCode("INVALID_REQUEST_ID");

        RuleFor(x => x.DestinationAccountNumber)
            .GreaterThan(0)
            .WithErrorCode("INVALID_DESTINATION_ACCOUNT_NUMBER");

        RuleFor(x => x.Amount)
            .GreaterThan(0)
            .WithErrorCode("INVALID_VALUE");

        RuleFor(x => x.UserId)
            .NotEmpty().WithErrorCode("USER_IS_REQUIRED")
            .NotNull().WithErrorCode("USER_IS_REQUIRED");
    }
}