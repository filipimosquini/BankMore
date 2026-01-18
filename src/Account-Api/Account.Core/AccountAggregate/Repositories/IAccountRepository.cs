using Account.Core.Commom.Repositories.Bases;
using System;
using System.Threading.Tasks;

namespace Account.Core.AccountAggregate.Repositories;

public interface IAccountRepository : IBaseRepository<Account>
{
    Task<Core.AccountAggregate.Account> GetById(Guid id);
}