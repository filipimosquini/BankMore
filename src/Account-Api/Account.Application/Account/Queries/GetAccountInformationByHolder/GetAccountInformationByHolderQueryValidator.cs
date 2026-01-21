using FluentValidation;

namespace Account.Application.Account.Queries.GetAccountInformationByHolder;

public class GetAccountInformationByHolderQueryValidator : AbstractValidator<GetAccountInformationByHolderQuery>
{
    public GetAccountInformationByHolderQueryValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithErrorCode("USER_IS_REQUIRED")
            .NotNull().WithErrorCode("USER_IS_REQUIRED");
    }
}