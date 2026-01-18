using System;
using System.Collections.Generic;
using System.Linq;
using Account.Core.MovementAggregate;
using Account.Core.MovementAggregate.Repositories;
using Account.Infrastructure.Contexts;
using Account.Infrastructure.Repositories.Bases;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Account.Infrastructure.Repositories;

public class MovementRepository : BaseRepository<Movement>, IMovementRepository
{
    private readonly DatabaseContext _context;

    public MovementRepository(DatabaseContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Movement>> GetByAccountIdAsync(Guid accountId)
        => await _context.Movements.Where(x => x.AccountId == accountId).ToListAsync();
}