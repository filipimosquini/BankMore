using Account.Core.Commom.Repositories.Bases;
using System;
using System.Threading.Tasks;

namespace Account.Core.AccountAggregate.Repositories;

public interface IAccountRepository : IBaseRepository<Account>
{
    Task<Core.AccountAggregate.Account> GetById(Guid id);
    Task<Core.AccountAggregate.Account> GetByUser(Guid userId);
    Task<Core.AccountAggregate.Account> GetByAccountNumber(int accountNumber);
}