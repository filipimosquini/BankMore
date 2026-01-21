using FluentValidation;

namespace Account.Application.Account.Queries.GetAccountInformation;

public class GetAccountInformationQueryValidator : AbstractValidator<GetAccountInformationQuery>
{
    public GetAccountInformationQueryValidator()
    {
        RuleFor(x => x.AccountNumber)
            .GreaterThan(0)
            .WithErrorCode("INVALID_ACCOUNT_NUMBER");
    }
}