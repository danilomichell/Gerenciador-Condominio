using Condominio.Data.Context;
using Condominio.Domain.Interfaces.Repositories;

namespace Condominio.Data.Persistence;

public interface IUnitOfWorkImdb
{
    CondominioContext Context { get; }

    IUsuarioRepository UsuarioRepository { get; }

    void BeginTransaction();

    void CommitTransaction();

    void Save();

    void Add(object entity);

    Task AddAsync(object entity);

    void Update(object entity);

    Task UpdateAsync(object entity);
}