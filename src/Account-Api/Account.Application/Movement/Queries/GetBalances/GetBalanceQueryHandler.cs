using Account.Application.Movement.Dto;
using Account.Application.Services;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Account.Application.Movement.Queries.GetBalances;

public class GetBalanceQueryHandler(IMovementService movementService, IAccountService accountService) : IRequestHandler<GetBalanceQuery, GetBalanceQueryDto>
{
    public async Task<GetBalanceQueryDto> Handle(GetBalanceQuery query, CancellationToken cancellationToken)
    {
        var account = await accountService.ValidateAccountAsync(query.AccountNumber);

        return new GetBalanceQueryDto
        {
            AccountNumber = account.Number,
            Holder = account.Holder,
            QueryDate = DateTime.UtcNow,
            Balance = await movementService.GetBalanceAsync(account.Id)
        };
    }
}