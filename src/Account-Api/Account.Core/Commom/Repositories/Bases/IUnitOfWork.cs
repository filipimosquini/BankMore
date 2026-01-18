using System.Threading.Tasks;

namespace Account.Core.Commom.Repositories.Bases;

public interface IUnitOfWork
{
    Task<bool> Commit();
}