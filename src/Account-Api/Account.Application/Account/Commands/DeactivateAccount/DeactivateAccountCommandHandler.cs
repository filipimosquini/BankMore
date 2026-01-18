using Account.Core.AccountAggregate.Repositories;
using Account.Infrastructure.CrossCutting.Exceptions;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Account.Application.Account.Commands.DeactivateAccount;

public class DeactivateAccountCommandHandler(IAccountRepository accountRepository) : IRequestHandler<DeactivateAccountCommand, Unit>
{
    public async Task<Unit> Handle(DeactivateAccountCommand request, CancellationToken cancellationToken)
    {
        var account = await accountRepository.GetByUser(Guid.Parse(request.UserId));

        if (account is null)
        {
            throw new AccountNotFoundException();
        }

        account.DisableAccount();

        accountRepository.Update(account);

        await accountRepository.UnitOfWork.Commit(cancellationToken);

        return Unit.Value;
    }
}