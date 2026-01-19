using System.Threading;
using System.Threading.Tasks;

namespace Transfer.Core.Common.Repositories.Bases;

public interface IUnitOfWork
{
    Task<bool> Commit(CancellationToken cancellationToken);
}