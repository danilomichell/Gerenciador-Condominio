namespace Condominio.Service.Features.Query.RealizarLogin;

public class RealizarLoginResult
{
    public RealizarLoginResult(string token, string nome, string email, string cargo)
    {
        Token = token;
        Nome = nome;
        Email = email;
        Cargo = cargo;
    }

    public string Nome { get; set; }
    public string Email { get; set; }
    public string Cargo { get; set; }
    public string Token { get; set; }
}