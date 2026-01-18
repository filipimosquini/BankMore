using Account.Core.AccountAggregate.Repositories;
using Account.Infrastructure.CrossCutting.Exceptions;
using System.Threading.Tasks;

namespace Account.Application.Services;

public class AccountService(IAccountRepository accountRepository) : IAccountService
{
    public async Task<Core.AccountAggregate.Account> ValidateAccountAsync(int accountNumber)
    {
        var account = await accountRepository.GetByAccountNumber(accountNumber);

        if (account is null)
        {
            throw new AccountNotFoundException();
        }

        if (!account.Active)
        {
            throw new AccountInactiveException();
        }

        return account;
    }
}