using System.Collections.Generic;
using System.Threading.Tasks;

namespace Transfer.Core.Repositories.Bases;

public interface IBaseRepository<TEntity> where TEntity : class
{
    IUnitOfWork UnitOfWork { get; }
    TEntity Add(TEntity entity);
    void AddRange(ICollection<TEntity> entity);
    TEntity FindById(params object[] keyValues);
    TEntity Update(TEntity entity);
    void UpdateRange(ICollection<TEntity> entities);
    TEntity Remove(TEntity entity);

    Task<TEntity> AddAsync(TEntity entity);
    Task AddRangeAsync(ICollection<TEntity> entity);
    Task<TEntity> FindById(object id);
    Task<IEnumerable<TEntity>> GetAllAsync();
}