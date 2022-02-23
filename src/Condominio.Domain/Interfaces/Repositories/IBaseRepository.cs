using System.Linq.Expressions;

namespace Condominio.Domain.Interfaces.Repositories;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task<IQueryable<TEntity>> GetAll();
    Task<IQueryable<TEntity>> GetAllWithIncludes(params string[] includes);
    Task<IQueryable<TEntity>> GetAllWithIncludes(Expression<Func<TEntity, bool>> query, params string[] includes);
    Task<TEntity?> GetById(int id);
    Task<TEntity> Insert(TEntity obj);
    Task<TEntity> Update(TEntity obj);
    Task<int> SaveChanges();
}