using System;
using System.Threading.Tasks;
using Account.Core.AccountAggregate.Repositories;
using Account.Infrastructure.Contexts;
using Account.Infrastructure.Repositories.Bases;
using Microsoft.EntityFrameworkCore;

namespace Account.Infrastructure.Repositories;

public class AccountRepository(DatabaseContext context) : BaseRepository<Core.AccountAggregate.Account>(context), IAccountRepository
{
    public async Task<Core.AccountAggregate.Account> GetById(Guid id)
        => await context.Accounts.FirstOrDefaultAsync(x => x.Id == id);

    public async Task<Core.AccountAggregate.Account> GetByUser(Guid userId)
        => await context.Accounts.FirstOrDefaultAsync(x => x.UserId == userId);

    public async Task<Core.AccountAggregate.Account> GetByAccountNumber(int accountNumber)
        => await context.Accounts.FirstOrDefaultAsync(x => x.Number == accountNumber);
}