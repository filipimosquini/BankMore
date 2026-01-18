using System;
using System.Threading.Tasks;
using Account.Core.AccountAggregate.Repositories;
using Account.Infrastructure.Contexts;
using Account.Infrastructure.Repositories.Bases;
using Microsoft.EntityFrameworkCore;

namespace Account.Infrastructure.Repositories;

public class AccountRepository : BaseRepository<Core.AccountAggregate.Account>, IAccountRepository
{
    private readonly DatabaseContext _context;

    public AccountRepository(DatabaseContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Core.AccountAggregate.Account> GetById(Guid id)
        => await _context.Accounts.FirstOrDefaultAsync(x => x.Id == id);
}