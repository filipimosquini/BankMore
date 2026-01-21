using Account.Application.Account.Dto;
using Account.Application.Services;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Account.Application.Account.Queries.GetAccountInformationByHolder;

public class GetAccountInformationByHolderQueryHandler(IAccountService accountService)
    : IRequestHandler<GetAccountInformationByHolderQuery, GetAccountInformationDto>
{
    public async Task<GetAccountInformationDto> Handle(GetAccountInformationByHolderQuery query, CancellationToken cancellationToken)
    {
        var account = await accountService.ValidateAccountAsync(Guid.Parse(query.UserId));

        return new(account.Id, account.Number);
    }
}