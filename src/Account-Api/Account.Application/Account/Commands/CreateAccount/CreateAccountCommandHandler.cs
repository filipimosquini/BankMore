using System;
using System.Threading;
using System.Threading.Tasks;
using Account.Core.AccountAggregate.Repositories;
using MediatR;

namespace Account.Application.Account.Commands.CreateAccount;

public class CreateAccountCommandHandler(IAccountRepository accountRepository) : IRequestHandler<CreateAccountCommand, Unit>
{
    public async Task<Unit> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var account = new Core.AccountAggregate.Account(request.Cpf, Guid.Parse(request.UserId));

        await accountRepository.AddAsync(account);

        await accountRepository.UnitOfWork.Commit();

        return Unit.Value;
    }
}