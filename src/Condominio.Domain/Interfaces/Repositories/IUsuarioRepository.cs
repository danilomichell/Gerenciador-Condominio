using Condominio.Domain.Entities;

namespace Condominio.Domain.Interfaces.Repositories;

public interface IUsuarioRepository : IBaseRepository<Usuario>
{
    Task<bool> ExisteUsuarioCadastrado(string email);
    Task<Usuario?> ObterUsuarioPorEmail(string email);
}