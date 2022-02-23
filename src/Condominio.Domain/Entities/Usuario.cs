namespace Condominio.Domain.Entities;

public class Usuario : Entity
{
    public Usuario(string nome, string email, string senha, EnumCargo cargo)
    {
        Nome = nome;
        Email = email;
        Senha = senha;
        Cargo = cargo;
    }

    public string Nome { get; }
    public string Email { get; }
    public string Senha { get; }
    public EnumCargo Cargo { get; }
}