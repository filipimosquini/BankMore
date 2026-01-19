using Account.Application.Account.Dto;
using Account.Core.AccountAggregate.Repositories;
using Account.Infrastructure.CrossCutting.Exceptions;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Account.Application.Account.Commands.CreateAccount;

public class CreateAccountCommandHandler(IAccountRepository accountRepository) : IRequestHandler<CreateAccountCommand, CreateAccountDto>
{
    public async Task<CreateAccountDto> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var registeredAccount = await accountRepository.GetByUser(Guid.Parse(request.UserId));
        if (registeredAccount is not null)
        {
            throw new UserHasRegisteredAccountConflictException();
        }

        var account = new Core.AccountAggregate.Account(request.Holder, Guid.Parse(request.UserId));

        await accountRepository.AddAsync(account);

        await accountRepository.UnitOfWork.Commit(cancellationToken);
        
        return new CreateAccountDto
        {
            AccountNumber = account.Number
        };
    }
}