using Account.Application.Account.Dto;
using Account.Application.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Account.Application.Account.Queries.GetAccountInformation;

public class GetAccountInformationQueryHandler(IAccountService accountService)
    : IRequestHandler<GetAccountInformationQuery, GetAccountInformationDto>
{
    public async Task<GetAccountInformationDto> Handle(GetAccountInformationQuery query, CancellationToken cancellationToken)
    {
        var account = await accountService.ValidateAccountAsync(query.AccountNumber);

        return new (account.Id, account.Number);
    }
}