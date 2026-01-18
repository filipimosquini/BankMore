using System;
using System.Threading.Tasks;
using Account.Core.Common.Repositories.Bases;

namespace Account.Core.AccountAggregate.Repositories;

public interface IAccountRepository : IBaseRepository<Account>
{
    Task<Core.AccountAggregate.Account> GetById(Guid id);
    Task<Core.AccountAggregate.Account> GetByUser(Guid userId);
    Task<Core.AccountAggregate.Account> GetByAccountNumber(int accountNumber);
}