using Condominio.Domain.Entities;
using Condominio.Service.Features.Query.RealizarLogin;

namespace Condominio.Service.Services.Interface;

public interface IUsuarioService
{
    Task CadastrarUsuario(string nome, string email, string senha, EnumCargo cargo);
    Task<RealizarLoginResult> Login(string email, string senha);
}