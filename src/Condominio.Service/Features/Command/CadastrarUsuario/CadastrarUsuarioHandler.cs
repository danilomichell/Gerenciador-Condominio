using Condominio.Data.Persistence;
using Condominio.Service.Services.Interface;
using MediatR;

namespace Condominio.Service.Features.Command.CadastrarUsuario;

public class CadastrarUsuarioHandler : IRequestHandler<CadastrarUsuarioCommand>
{
    private readonly IUnitOfWorkImdb _unitOfWorkImdb;
    private readonly IUsuarioService _usuarioService;

    public CadastrarUsuarioHandler(IUnitOfWorkImdb unitOfWorkImdb, IUsuarioService usuarioService)
    {
        _unitOfWorkImdb = unitOfWorkImdb;
        _usuarioService = usuarioService;
    }

    public async Task<Unit> Handle(CadastrarUsuarioCommand request, CancellationToken cancellationToken)
    {
        if (await _unitOfWorkImdb.UsuarioRepository.ExisteUsuarioCadastrado(request.Email))
            throw new OperationCanceledException($"Já existe com usuário cadastrado com o email {request.Email}");
        await _usuarioService.CadastrarUsuario(request.Nome, request.Email, request.Senha, request.Cargo);

        return await Task.FromResult(Unit.Value);
    }
}