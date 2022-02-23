using Condominio.Data.Context;
using Condominio.Domain.Interfaces.Repositories;

namespace Condominio.Data.Persistence;

public class UnitOfWorkImdb : IUnitOfWorkImdb, IDisposable
{
    private bool _disposed;

    public UnitOfWorkImdb(
        CondominioContext imdbContext,
        IUsuarioRepository usuarioRepository)
    {
        Context = imdbContext;
        UsuarioRepository = usuarioRepository;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public CondominioContext Context { get; }

    public IUsuarioRepository UsuarioRepository { get; }

    public void Save()
    {
        Context.SaveChanges();
    }

    public void BeginTransaction()
    {
        Context.Database.BeginTransaction();
    }

    public void CommitTransaction()
    {
        Context.Database.CommitTransaction();
    }

    public void Add(object entity)
    {
        Context.Add(entity);
    }

    public async Task AddAsync(object entity)
    {
        await Context.AddAsync(entity);
    }

    public void Update(object entity)
    {
        Context.Update(entity);
    }

    public async Task UpdateAsync(object entity)
    {
        Context.Update(entity);
        await Task.CompletedTask;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed && disposing) Context.Dispose();
        _disposed = true;
    }
}