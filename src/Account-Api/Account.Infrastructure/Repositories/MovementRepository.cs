using Account.Core.MovementAggregate;
using Account.Core.MovementAggregate.Repositories;
using Account.Infrastructure.Contexts;
using Account.Infrastructure.Repositories.Bases;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Account.Infrastructure.Repositories;

public class MovementRepository(DatabaseContext context) : BaseRepository<Movement>(context), IMovementRepository
{
    public async Task<IEnumerable<Movement>> GetByAccountIdAsync(Guid accountId)
        => await context.Movements.Where(x => x.AccountId == accountId).ToListAsync();
}