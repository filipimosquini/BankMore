using FluentValidation;

namespace Account.Application.Movement.Queries.GetBalances;

public class GetBalanceQueryValidator : AbstractValidator<GetBalanceQuery>
{
    public GetBalanceQueryValidator()
    {
        RuleFor(x => x.AccountNumber)
            .GreaterThan(0)
            .WithErrorCode("INVALID_ACCOUNT_NUMBER");
    }
}