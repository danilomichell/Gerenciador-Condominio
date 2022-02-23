using MediatR;

namespace Condominio.Service.Features.Query.RealizarLogin;

public class RealizarLoginQuery : IRequest<RealizarLoginResult>
{
    public RealizarLoginQuery(string email, string senha)
    {
        Email = email;
        Senha = senha;
    }

    public string Email { get; set; }
    public string Senha { get; set; }
}