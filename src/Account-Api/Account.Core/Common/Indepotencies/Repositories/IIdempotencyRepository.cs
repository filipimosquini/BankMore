using Account.Core.Common.Indepotencies.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;
using Account.Core.Common.Repositories.Bases;

namespace Account.Core.Common.Indepotencies.Repositories;

public interface IIdempotencyRepository : IBaseRepository<Idempotency>
{
    Task<bool> TryBeginAsync(Guid id, string requestHash, CancellationToken cancellationToken);
    Task<Idempotency?> GetAsync(Guid id, CancellationToken cancellationToken);
    Task CompleteAsync(Guid id, string result, CancellationToken cancellationToken);
}