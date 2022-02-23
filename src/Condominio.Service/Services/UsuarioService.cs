using Condominio.Data.Persistence;
using Condominio.Domain.Entities;
using Condominio.Domain.Interfaces.Util;
using Condominio.Service.Features.Query.RealizarLogin;
using Condominio.Service.Services.Interface;
using Condominio.Util.Extensions;

namespace Condominio.Service.Services;

public class UsuarioService : IUsuarioService
{
    private readonly ICryptograph _cryptograph;
    private readonly ITokenService _tokenService;
    private readonly IUnitOfWorkImdb _unitOfWorkImdb;

    public UsuarioService(IUnitOfWorkImdb unitOfWorkImdb,
        ICryptograph cryptograph,
        ITokenService tokenService)
    {
        _unitOfWorkImdb = unitOfWorkImdb;
        _cryptograph = cryptograph;
        _tokenService = tokenService;
    }

    public async Task CadastrarUsuario(string nome, string email, string senha, EnumCargo cargo)
    {
        await _unitOfWorkImdb.UsuarioRepository.Insert(new Usuario
        (
            nome,
            email,
            _cryptograph.EncryptPassword(senha),
            cargo
        ));

        _unitOfWorkImdb.Save();
    }

    public async Task<RealizarLoginResult> Login(string email, string senha)
    {
        var usuario = await _unitOfWorkImdb.UsuarioRepository.ObterUsuarioPorEmail(email);
        if (usuario == null || !_cryptograph.VerifyPassword(senha, usuario.Senha))
            throw new OperationCanceledException("Email ou senha invalidos.");
        if (!usuario.Active) throw new OperationCanceledException("Cadastro ainda não foi aprovado.");
        var tokenCreate = await _tokenService.TokenGenerate(usuario.Id, usuario.Nome, usuario.Cargo);

        return new RealizarLoginResult
        (
            nome: usuario.Nome,
            email: usuario.Email,
            cargo: usuario.Cargo.GetDescription(),
            token: tokenCreate
        );
    }
}