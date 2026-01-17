using System.Threading.Tasks;

namespace Transfer.Core.Repositories.Bases;

public interface IUnitOfWork
{
    Task<bool> Commit();
}