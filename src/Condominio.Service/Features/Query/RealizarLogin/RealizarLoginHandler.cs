using Condominio.Data.Persistence;
using Condominio.Service.Services.Interface;
using MediatR;

namespace Condominio.Service.Features.Query.RealizarLogin;

public class RealizarLoginHandler : IRequestHandler<RealizarLoginQuery, RealizarLoginResult>
{
    private readonly IUnitOfWorkImdb _unitOfWorkImdb;
    private readonly IUsuarioService _usuarioService;

    public RealizarLoginHandler(IUnitOfWorkImdb unitOfWorkImdb, IUsuarioService usuarioService)
    {
        _unitOfWorkImdb = unitOfWorkImdb;
        _usuarioService = usuarioService;
    }

    public async Task<RealizarLoginResult> Handle(RealizarLoginQuery request, CancellationToken cancellationToken)
    {
        if (!await _unitOfWorkImdb.UsuarioRepository.ExisteUsuarioCadastrado(request.Email))
            throw new OperationCanceledException($"Não existe um usuário cadastrado com o email {request.Email}");

        return await _usuarioService.Login(request.Email, request.Senha);
    }
}