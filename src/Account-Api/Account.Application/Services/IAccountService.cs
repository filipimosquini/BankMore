using System.Threading.Tasks;

namespace Account.Application.Services;

public interface IAccountService
{
    Task<Core.AccountAggregate.Account> ValidateAccountAsync(int accountNumber);
}