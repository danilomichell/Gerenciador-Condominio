using Condominio.Api.Filter;
using Condominio.Api.Model;
using Condominio.Domain.Entities;
using Condominio.Service.Features.Command.CadastrarUsuario;
using Condominio.Service.Features.Query.RealizarLogin;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Condominio.Api.Controllers;

/// <summary>
///     Controller do usuario
/// </summary>
[Authorize]
[Route("[controller]")]
[ServiceFilter(typeof(ApiExceptionFilterAttribute))]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsuarioController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    /// <summary>
    ///     Endpoint responsável por cadastrar um usuário
    /// </summary>
    /// <param name="registrarUsuarioModel"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [ProducesResponseType(typeof(CadastrarUsuarioModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [HttpPost("Registrar")]
    public async Task<IActionResult> CriarUsuario(CadastrarUsuarioModel registrarUsuarioModel)
    {
        await _mediator.Send(new CadastrarUsuarioCommand
        (
            registrarUsuarioModel.Nome,
            registrarUsuarioModel.Email,
            registrarUsuarioModel.Senha,
            EnumCargo.MORADOR
        ));

        return Created(Request.GetDisplayUrl(), registrarUsuarioModel);
    }

    /// <summary>
    ///     Endpoint responsável por realizar o login
    /// </summary>
    /// <param name="loginModel"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [ProducesResponseType(typeof(RealizarLoginModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [HttpPost("Login")]
    public async Task<IActionResult> LoginUser(RealizarLoginModel loginModel)
    {
        var login = await _mediator.Send(new RealizarLoginQuery
        (
            loginModel.Email,
            loginModel.Senha
        ));

        return Ok(login);
    }
}