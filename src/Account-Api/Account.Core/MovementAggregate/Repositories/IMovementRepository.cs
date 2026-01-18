using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Account.Core.Common.Repositories.Bases;

namespace Account.Core.MovementAggregate.Repositories;

public interface IMovementRepository : IBaseRepository<Movement>
{
    Task<IEnumerable<Movement>> GetByAccountIdAsync(Guid accountId);
}