using Account.Core.Commom.Repositories.Bases;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Account.Core.MovementAggregate.Repositories;

public interface IMovementRepository : IBaseRepository<Movement>
{
    Task<IEnumerable<Movement>> GetByAccountIdAsync(Guid accountId);
}