using Condominio.Data.Context;
using Condominio.Domain.Entities;
using Condominio.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Condominio.Data.Repositories;

public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
{
    public UsuarioRepository(CondominioContext context) : base(context)
    {
    }

    public async Task<bool> ExisteUsuarioCadastrado(string email)
    {
        var usuario = await Context.Usuarios.FirstOrDefaultAsync(x => x.Email == email);
        if (usuario is not null)
            return await Task.FromResult(true);
        return await Task.FromResult(false);
    }

    public async Task<Usuario?> ObterUsuarioPorEmail(string email)
    {
        return await Context.Usuarios.FirstOrDefaultAsync(x => x.Email == email);
    }
}