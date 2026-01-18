using Account.Application.Account.Dto;
using Account.Core.AccountAggregate.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Account.Application.Account.Commands.CreateAccount;

public class CreateAccountCommandHandler(IAccountRepository accountRepository) : IRequestHandler<CreateAccountCommand, CreateAccountDto>
{
    public async Task<CreateAccountDto> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var account = new Core.AccountAggregate.Account(request.Holder, Guid.Parse(request.UserId));

        await accountRepository.AddAsync(account);

        await accountRepository.UnitOfWork.Commit();

        return new CreateAccountDto
        {
            AccountNumber = account.Number
        };
    }
}