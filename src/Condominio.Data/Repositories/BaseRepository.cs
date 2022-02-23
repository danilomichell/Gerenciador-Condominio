using System.Linq.Expressions;
using Condominio.Data.Context;
using Condominio.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Condominio.Data.Repositories;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
    where TEntity : class
{
    private readonly DbSet<TEntity> _dbSet;
    protected readonly CondominioContext Context;

    protected BaseRepository(CondominioContext context)
    {
        Context = context;
        _dbSet = context.Set<TEntity>();
    }

    public async Task<IQueryable<TEntity>> GetAll()
    {
        return await Task.FromResult(_dbSet.AsQueryable());
    }

    public async Task<IQueryable<TEntity>> GetAllWithIncludes
        (params string[] includes)
    {
        var result = _dbSet.AsQueryable();
        result = includes.Aggregate(result, (current, i) => current.Include(i));
        return await Task.FromResult(result);
    }

    public async Task<IQueryable<TEntity>> GetAllWithIncludes
        (Expression<Func<TEntity, bool>> query, params string[] includes)
    {
        var result = _dbSet.Where(query).AsQueryable();
        result = includes.Aggregate(result, (current, i) => current.Include(i));
        return await Task.FromResult(result);
    }

    public async Task<TEntity?> GetById(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<TEntity> Insert(TEntity obj)
    {
        var entity = await _dbSet.AddAsync(obj);
        return entity.Entity;
    }

    public async Task<TEntity> Update(TEntity obj)
    {
        return await Task.FromResult(_dbSet.Update(obj).Entity);
    }

    public async Task<int> SaveChanges()
    {
        return await Context.SaveChangesAsync();
    }
}