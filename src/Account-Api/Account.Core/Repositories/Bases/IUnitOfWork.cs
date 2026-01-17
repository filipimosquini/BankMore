using System.Threading.Tasks;

namespace Account.Core.Repositories.Bases;

public interface IUnitOfWork
{
    Task<bool> Commit();
}