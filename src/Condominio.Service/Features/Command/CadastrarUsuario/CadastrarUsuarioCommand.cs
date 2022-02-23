using Condominio.Domain.Entities;
using MediatR;

namespace Condominio.Service.Features.Command.CadastrarUsuario;

public class CadastrarUsuarioCommand : IRequest
{
    public CadastrarUsuarioCommand(string nome, string email, string senha, EnumCargo cargo)
    {
        Nome = nome;
        Email = email;
        Senha = senha;
        Cargo = cargo;
    }

    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public EnumCargo Cargo { get; set; }
}