using FluentValidation;

namespace Condominio.Service.Features.Command.CadastrarUsuario;

public class CadastrarUsuarioValidator : AbstractValidator<CadastrarUsuarioCommand>
{
    public CadastrarUsuarioValidator()
    {
        RuleFor(p => p.Nome)
            .NotEmpty().WithMessage("O Nome precisa ser informado.");

        RuleFor(r => r.Email)
            .NotEmpty().WithMessage("O Email precisa ser informado.");

        RuleFor(r => r.Senha)
            .NotEmpty().WithMessage("A Senha precisa ser informado.");

        RuleFor(r => r.Cargo)
            .NotEmpty().WithMessage("O Cargo precisa ser informado.")
            .IsInEnum().WithMessage("O Cargo informado é inválido.");
    }
}