using Account.Core.Commom.Repositories.Bases;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Account.Infrastructure.Repositories.Bases;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    public DbContext Context { get; }
    public IUnitOfWork UnitOfWork { get; }

    internal readonly DbSet<TEntity> DbSet;


    public BaseRepository(DbContext context)
    {
        Context = context;
        DbSet = Context.Set<TEntity>();
        UnitOfWork = Context as IUnitOfWork;
    }

    public TEntity Add(TEntity entity)
    {
        DbSet.Add(entity);
        return entity;
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await DbSet.AddAsync(entity);
        return entity;
    }

    public void AddRange(ICollection<TEntity> entities)
    {
        DbSet.AddRange(entities);
    }

    public async Task AddRangeAsync(ICollection<TEntity> entities)
    {
        await DbSet.AddRangeAsync(entities);
    }

    public TEntity Remove(TEntity entity)
    {
        DbSet.Remove(entity);

        return entity;
    }

    public TEntity FindById(params object[] ids)
    {
        return DbSet.Find(ids);
    }
    public async Task<TEntity> FindById(object id)
    {
        return await DbSet.FindAsync(id);
    }

    public virtual TEntity Update(TEntity entity)
    {
        var entry = Context.Entry(entity);
        DbSet.Attach(entity);
        entry.State = EntityState.Modified;

        return entity;
    }

    public void UpdateRange(ICollection<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            var entry = Context.Entry(entity);
            DbSet.Attach(entity);
            entry.State = EntityState.Modified;
        }
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await DbSet.ToListAsync();
    }
}