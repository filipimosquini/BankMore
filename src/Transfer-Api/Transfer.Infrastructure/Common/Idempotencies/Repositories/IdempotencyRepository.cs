using Transfer.Core.Common.Indepotencies.Entities;
using Transfer.Core.Common.Indepotencies.Repositories;
using Transfer.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Threading;
using System.Threading.Tasks;
using Transfer.Infrastructure.Repositories.Bases;

namespace Transfer.Infrastructure.Common.Idempotencies.Repositories;

public class IdempotencyRepository(DatabaseContext context) : BaseRepository<Idempotency>(context), IIdempotencyRepository
{
    public async Task<bool> TryBeginAsync(Guid id, string requestHash, CancellationToken cancellationToken)
    {
        var entity = new Idempotency(id);
        entity.AddRequestHash(requestHash);

        await context.AddAsync(entity, cancellationToken);

        try
        {
            await UnitOfWork.Commit(cancellationToken);
            return true;
        }
        catch (DbUpdateException ex) when (IsUniqueConstraintViolation(ex))
        {
            context.ChangeTracker.Clear();
            return false;
        }
    }

    public Task<Idempotency?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        return context.Set<Idempotency>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task CompleteAsync(Guid id, string result, CancellationToken cancellationToken)
    {
        var entity = await context.Set<Idempotency>()
            .FirstAsync(x => x.Id == id, cancellationToken);

        entity.AddResult(result);

        await UnitOfWork.Commit(cancellationToken);
    }

    private static bool IsUniqueConstraintViolation(DbUpdateException ex)
    {
        return ex.InnerException is OracleException oracleEx && oracleEx.Number == 1; // ORA-00001
    }
}