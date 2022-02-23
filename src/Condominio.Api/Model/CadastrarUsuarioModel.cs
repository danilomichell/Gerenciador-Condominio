#nullable disable


using System.ComponentModel.DataAnnotations;

namespace Condominio.Api.Model;

public class CadastrarUsuarioModel
{
    [Required(ErrorMessage = "Nome precisa ser fornecido")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "Email precisa ser fornecido")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Senha precisa ser fornecida")]
    public string Senha { get; set; }

    [Required(ErrorMessage = "Senha de confirmação é necessária")]
    [Compare("Senha", ErrorMessage = "Senhas não coincidem")]
    public string ConfirmarSenha { get; set; }
}