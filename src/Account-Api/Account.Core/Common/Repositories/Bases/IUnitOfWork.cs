using System.Threading;
using System.Threading.Tasks;

namespace Account.Core.Common.Repositories.Bases;

public interface IUnitOfWork
{
    Task<bool> Commit(CancellationToken cancellationToken);
}